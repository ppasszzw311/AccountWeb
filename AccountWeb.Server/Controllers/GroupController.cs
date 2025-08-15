using AccountWeb.Server.Data;
using AccountWeb.Server.Models;
using AccountWeb.Server.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountWeb.Server.Controllers;

[ApiController]
[Route("api/groups")]
public class GroupController: ControllerBase
{
    private readonly AppDbContext _context;
    public GroupController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups()
    {
        var groups = await _context.Groups
            .Include(g => g.Project)
            .ThenInclude(p => p.Factory)
            .Select(g => new
            {
                g.Id,
                g.GroupName,
                g.Description,
                ProjectName = g.Project.ProjectName,
                FactoryName = g.Project.Factory.FactoryName,
                g.CreatedAt,
                g.UpdatedAt,
                MemberCount = g.UserGroupMappings.Count
            })
            .ToListAsync();
        return Ok(groups);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.GroupName) || request.ProjectId <= 0)
        {
            return BadRequest("Invalid group data.");
        }
        var group = new Models.Group
        {
            GroupName = request.GroupName,
            ProjectId = request.ProjectId,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _context.Groups.Add(group);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetGroups), new { id = group.Id }, group);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGroup(int id, [FromBody] UpdateGroupRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.GroupName) || request.ProjectId <= 0)
        {
            return BadRequest("Invalid group data.");
        }
        var group = await _context.Groups.FindAsync(id);
        if (group == null)
        {
            return NotFound();
        }
        group.GroupName = request.GroupName;
        group.ProjectId = request.ProjectId;
        group.Description = request.Description;
        group.UpdatedAt = DateTime.UtcNow;
        _context.Groups.Update(group);
        await _context.SaveChangesAsync();
        return Ok(new { message = "群組資料已更新"});
    } 

    [HttpGet("{id}/users")]
    public async Task<ActionResult<IEnumerable<object>>> GetGroupUsers(int id)
    {
        var users = await _context.UserGroupMappings
            .Include(ugm => ugm.User)
            .ThenInclude(u => u.Factory)
            .Where(ugm => ugm.GroupId == id)
            .Select(ugm => new
            {
                ugm.Id,
                ugm.UserId,
                UserIdString = ugm.User.UserId,
                ugm.User.UserName,
                ugm.User.Email,
                ugm.User.FactoryId,
                FactoryName = ugm.User.Factory != null ? ugm.User.Factory.FactoryName : null,
                JoinedAt = ugm.CreatedAt
            })
            .ToListAsync();

        return Ok(users);
    }

    [HttpPost("{id}/users")]
    public async Task<IActionResult> AddUserToGroup(int id, [FromBody] AddUserToGroupRequest request)
    {
        // Check if group exists
        if (!await _context.Groups.AnyAsync(g => g.Id == id))
        {
            return NotFound(new { message = "群組不存在" });
        }

        // Check if user exists
        if (!await _context.Users.AnyAsync(u => u.Id == request.UserId))
        {
            return BadRequest(new { message = "使用者不存在" });
        }

        // Check if user is already in group
        if (await _context.UserGroupMappings.AnyAsync(ugm => ugm.GroupId == id && ugm.UserId == request.UserId))
        {
            return BadRequest(new { message = "使用者已在群組中" });
        }

        var userGroupMapping = new UserGroupMapping
        {
            GroupId = id,
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow
        };

        _context.UserGroupMappings.Add(userGroupMapping);
        await _context.SaveChangesAsync();

        return Ok(new { message = "已成功加入使用者至群組" });
    }

    [HttpDelete("{groupId}/users/{userId}")]
    public async Task<IActionResult> RemoveUserFromGroup(int groupId, int userId)
    {
        var userGroupMapping = await _context.UserGroupMappings
            .FirstOrDefaultAsync(ugm => ugm.GroupId == groupId && ugm.UserId == userId);

        if (userGroupMapping == null)
        {
            return NotFound(new { message = "使用者不在此群組中" });
        }

        _context.UserGroupMappings.Remove(userGroupMapping);
        await _context.SaveChangesAsync();

        return Ok(new { message = "已從群組中移除使用者" });
    }
}
