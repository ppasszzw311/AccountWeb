using AccountWeb.Server.Data;
using AccountWeb.Server.Models;
using AccountWeb.Server.Models.Dtos;
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
}
