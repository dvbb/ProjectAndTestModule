using Microsoft.EntityFrameworkCore;
using MyBankApiServerV2.Models;
using MyBankApiServerV2.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();

// cross-domain
builder.Services.AddCors(option =>
{
    option.AddPolicy("AnyPolicy",
        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

// EF Core
builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    string connStr = builder.Configuration.GetConnectionString("MyBankDBConnStr");
    connStr = connStr.Replace("FOO", Environment.GetEnvironmentVariable("PWD"));
    options.UseSqlServer();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors("AnyPolicy");
app.MapControllers();

app.Run();
