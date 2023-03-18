using ProgramTan.WebApi;

var builder = WebApplication.CreateBuilder(args);

var mongoDbConfig = builder.Configuration.GetSection("MongoDbConfig").Get<MongoDbConfig>();
builder.Services.AddSingleton(mongoDbConfig);

builder.Services.AddControllers();

#if DEBUG
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
#endif

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
#if DEBUG
app.UseCors("EnableCors");
#endif

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
