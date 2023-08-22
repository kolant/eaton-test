using EatonTest.DTO;
using EatonTest.Infrastructure;
using EatonTest.Infrastructure.Implementations;
using EatonTest.Infrastructure.Interfaces;
using EatonTest.Middleware;
using EatonTest.Services.Implementations;
using EatonTest.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("EatonTestDb"));
};

// Add services to the container.
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();

builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();

builder.Services.AddAutoMapper(new Type[] { typeof(DTOAutomapperProfile) });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
