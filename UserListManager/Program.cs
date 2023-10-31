using UserListManager.Models;


var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddDbContext<UsersContext>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
