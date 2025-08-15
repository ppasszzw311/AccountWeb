using AccountWeb.Server.Data;
using AccountWeb.Server.Models;
using AccountWeb.Server.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountWeb.Server.Controllers;

[ApiController]
[Route("api/factory")]
public class FactoryController : ControllerBase
{
    private readonly AppDbContext _context;
    public FactoryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetFactories()
    {
        var factories = await _context.Factories
            .Select(f => new
            {
                f.Id,
                f.FactoryId,
                f.FactoryName,
                f.CreatedAt,
                f.UpdatedAt,
                UserCount = f.Users.Count(),
                ProjectCount = f.Projects.Count()
            })
            .ToListAsync();

        return Ok(factories);
    }

    [HttpPost]
    public async Task<ActionResult<Factory>> CreateFactory([FromBody] CreateFactoryRequest request)
    {
        // Check if factory ID already exists
        if (await _context.Factories.AnyAsync(f => f.FactoryId == request.FactoryId))
        {
            return BadRequest(new { message = "廠區代碼已存在" });
        }

        var factory = new Factory
        {
            FactoryId = request.FactoryId,
            FactoryName = request.FactoryName,
            CreatedAt = DateTime.UtcNow
        };

        _context.Factories.Add(factory);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFactories), new { id = factory.Id }, factory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFactory(int id, [FromBody] UpdateFactoryRequest request)
    {
        var factory = await _context.Factories.FindAsync(id);

        if (factory == null)
        {
            return NotFound(new { message = "廠區不存在" });
        }

        // Check if new factory ID already exists (if changed)
        if (factory.FactoryId != request.FactoryId)
        {
            if (await _context.Factories.AnyAsync(f => f.FactoryId == request.FactoryId))
            {
                return BadRequest(new { message = "廠區代碼已存在" });
            }
        }

        factory.FactoryId = request.FactoryId;
        factory.FactoryName = request.FactoryName;
        factory.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new { message = "廠區資訊已更新" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFactory(int id)
    {
        var factory = await _context.Factories
            .Include(f => f.Users)
            .Include(f => f.Projects)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (factory == null)
        {
            return NotFound(new { message = "廠區不存在" });
        }

        // Check if factory has associated users or projects
        if (factory.Users.Any() || factory.Projects.Any())
        {
            return BadRequest(new { message = "無法刪除廠區，該廠區仍有關聯的使用者或專案" });
        }

        _context.Factories.Remove(factory);
        await _context.SaveChangesAsync();

        return Ok(new { message = "廠區已刪除" });
    }
}
