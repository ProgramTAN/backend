using ProgramTan.WebApi.Configs;
using ProgramTan.WebApi.Services;

var builder = WebApplication.CreateBuilder();

var mongoDbConfig = builder.Configuration.GetSection("MongoDbConfig").Get<MongoDbConfig>();
builder.Services.AddSingleton(mongoDbConfig);

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
app.UseAuthorization();
app.MapControllers();

app.Run();
