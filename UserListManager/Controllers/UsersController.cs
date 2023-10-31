using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserListManager.Models;

namespace UserListManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	private readonly UsersContext _context;

    public UsersController(UsersContext context)
    {
		_context = context;
	}

    [HttpGet]
	public IActionResult GetUsers()
	{
		return Ok(_context.Users);
	}
	[HttpGet("{id}")]
    public async Task<IActionResult> GetUsers(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
		if (user is null)
			return NotFound();

        return Ok(user);
    }

    [HttpPost]
	public async Task<IActionResult> AddUser([FromBody] User user)
	{
		await _context.Users.AddAsync(user);

		await _context.SaveChangesAsync();

		return Ok(user);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateUser(int id, [FromBody] User updating)
	{
		var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
		if (user is null)
			return NotFound();

		user.FirstName = updating.FirstName;
		user.LastName = updating.LastName;
		user.Age = updating.Age;
		user.Email = updating.Email;

		await _context.SaveChangesAsync();

		return Ok(user);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteUser(int id)
	{
		var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
		if (user is null)
			return NotFound();

		_context.Users.Remove(user);

		await _context.SaveChangesAsync();

		return Ok(user);
	}
}
