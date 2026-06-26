using DatingApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();  // ← tells .NET you're using controllers

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration
        .GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();


var app = builder.Build();
app.MapControllers();
app.UseCors(options => options
    .AllowAnyMethod().AllowAnyHeader()
    .WithOrigins("http://localhost:4200","https://localhost:4200"));



app.Run();
