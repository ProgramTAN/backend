using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProgramTan.WebApi.Configs;
using ProgramTan.WebApi.Dtos;
using ProgramTan.WebApi.Services;

namespace ProgramTan.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly JwtConfig jwtConfig;
	private readonly UserService userService;

	public AuthController(
		JwtConfig jwtConfig,
		UserService userService)
	{
		this.jwtConfig = jwtConfig;
		this.userService = userService;
	}

	[HttpPost("login")]
	public async Task<IActionResult> LoginAsync([FromBody] LoginDto login)
	{
		var users = await userService.GetAllAsync();
		var user = users.FirstOrDefault(u =>
			u.Username == login.Username &&
			u.Password == login.Password
		);

		if (user is null)
			return null;

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Name, user.Id)
			}),
			Expires = DateTime.UtcNow.AddMinutes(jwtConfig.Expiration),
			SigningCredentials = new(
				new SymmetricSecurityKey(
					Encoding.ASCII.GetBytes(jwtConfig.Secret)
				),
				SecurityAlgorithms.HmacSha256Signature
			)
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var securityToken = tokenHandler.CreateToken(tokenDescriptor);

		string token = tokenHandler.WriteToken(securityToken);

		if (string.IsNullOrWhiteSpace(token))
			return Unauthorized();

		return Ok(new
		{
			token,
			id = user.Id
		});
	}
}
