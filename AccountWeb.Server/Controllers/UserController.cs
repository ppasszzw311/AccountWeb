using AccountWeb.Server.Data;
using AccountWeb.Server.Models;
using AccountWeb.Server.Models.Dtos;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountWeb.Server.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    // getall
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync();
        var result = users.Select(user => new UserDto(user)).ToList();
        return Ok(result);
    }

    [HttpGet("userId")]
    public async Task<ActionResult<UserDto>> GetUserById(string userId)
    {
        var user = await _context.Users
            .Include(x => x.Factory)
            .FirstOrDefaultAsync(x => x.UserId == userId);
        if (user == null)
            return NotFound();
        return Ok(new UserDto(user));
    }

    // create
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = new User
        {
            UserId = request.UserId,
            UserName = request.UserName,
            Password = request.Password, // 密碼應該加密存儲
            FactoryId = request.FactoryId,
            Email = request.Email
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        var userDto = new UserDto(user);
        return CreatedAtAction(nameof(GetAllUsers), new { id = user.UserId }, userDto);
    }

    // edit user
    [HttpPut]
    public async Task<IActionResult> EditUser([FromBody] UpdateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == request.UserId);
        if (user == null)
            return NotFound(new { message = "使用者不存在" });

        if (!string.IsNullOrWhiteSpace(request.Email) && request.Email != user.Email)
        {
            // 先查詢信箱有沒有被使用
            var isEmail = await _context.Users.AnyAsync(x => x.Email == request.Email);
            if (!isEmail)
                user.Email = request.Email;
        }
        user.UserName = request.UserName;
        user.FactoryId = request.FactoryId;
        user.Factory = await _context.Factories.FirstOrDefaultAsync(x => x.Id == request.FactoryId);
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new { MessageProcessingHandler = "使用者更新成功", UserId = user.UserId });
    }

    // 刪除使用者
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return NotFound(new { message = "使用者不存在" });
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return Ok(new { message = "使用者刪除成功" });
    }

    // 重設密碼
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] Models.Dtos.ResetPasswordRequest request)
    {

        return Ok();
    }


    [HttpGet("{userId}/roles")]
    public async Task<ActionResult<IEnumerable<string>>> GetUserRoles(string userId)
    {
        var user = await _context.Users
            .Include(u => u.UserRoleMappings)
            .ThenInclude(urm => urm.Role)
            .FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
            return NotFound(new { message = "使用者不存在" });
        var roles = user.UserRoleMappings.Select(urm => urm.Role.RoleName).ToList();
        return Ok(roles);
    }

    [HttpGet("{userId}/groups")]
    public async Task<ActionResult<IEnumerable<string>>> GetUserGroups(string userId)
    {
        var user = await _context.Users
            .Include(u => u.UserGroupMappings)
            .ThenInclude(ugm => ugm.Group)
            .FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
            return NotFound(new { message = "使用者不存在" });
        var groups = user.UserGroupMappings.Select(ugm => ugm.Group.GroupName).ToList();
        return Ok(groups);
    }

}
