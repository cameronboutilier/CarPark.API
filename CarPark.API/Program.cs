using CarPark.API.Contracts;
using CarPark.API.Entities;
using CarPark.API.Models.Configuration;
using CarPark.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ParkingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});
builder.Services.AddTransient<IParkingService, ParkingService>();
builder.Services.AddTransient<IParkingRepository, ParkingRepository>();
builder.Services.Configure<ParkingCostConfig>(builder.Configuration.GetSection("ParkingCostConfig"));

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
