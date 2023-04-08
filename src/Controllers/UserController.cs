using Microsoft.AspNetCore.Mvc;
using ProgramTan.WebApi.Services;

namespace ProgramTan.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly UserService userService;

	public UserController(UserService userService) =>
		this.userService = userService;

	[HttpGet("{id}")]
	public async Task<IActionResult> GetByIdAsync(string id)
	{
		var user = await userService.GetByIdAsync(id);
		if (user is null)
			return NotFound();
		return Ok(user);
	}
}
