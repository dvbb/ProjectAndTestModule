using GameManagement.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// cross-domain
builder.Services.ConfigureCors();

// connect db
builder.Services.ConfigureSqlServerContext(builder.Configuration);

// Denpendency injection
builder.Services.ConfigureWrapper();
//builder.Services.ConfigureRedis(builder.Configuration);

// AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AnyPolicy"); // cross-domain

app.UseAuthorization();

app.MapControllers();

app.Run();
