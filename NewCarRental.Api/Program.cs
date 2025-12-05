using Microsoft.EntityFrameworkCore;
using NewCarRental.Application.Interfaces.Repositories;
using NewCarRental.Infrastructure.Contexts;
using NewCarRental.Infrastructure.Repositories;
using NewCarRental.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DK Category Repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// DK DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CarRentalDbContext>(options =>
options.UseSqlServer(connectionString));

// DK AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// DK MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(MappingProfile).Assembly));

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
