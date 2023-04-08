using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProgramTan.WebApi.Configs;
using ProgramTan.WebApi.Services;

var builder = WebApplication.CreateBuilder();

var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
var mongoDbConfig = builder.Configuration.GetSection("MongoDbConfig").Get<MongoDbConfig>();
var pepper = builder.Configuration["Pepper"];

builder.Services.AddSingleton(mongoDbConfig);
builder.Services.AddSingleton(jwtConfig);
builder.Services.AddSingleton(pepper);

builder.Services.AddSingleton<UserService>();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddCors(option =>
{
	option.AddPolicy("EnableCors", policy =>
	{
		policy
			.SetIsOriginAllowed(origin => true)
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowCredentials()
			.Build();
	});
});

builder.Services
	.AddAuthentication(option =>
	{
		option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(option =>
	{
		option.SaveToken = true;
		option.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.ASCII.GetBytes(jwtConfig.Secret)
			),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseCors("EnableCors");
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
