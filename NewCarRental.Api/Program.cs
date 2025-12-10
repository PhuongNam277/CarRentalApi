using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewCarRental.Application.Interfaces.Authentication;
using NewCarRental.Application.Interfaces.Repositories;
using NewCarRental.Application.Mappings;
using NewCarRental.Domain.Settings;
using NewCarRental.Infrastructure.Authentication;
using NewCarRental.Infrastructure.Contexts;
using NewCarRental.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Get appsettings config
var jwtSettings = new JwtSettings();
builder.Configuration.Bind(JwtSettings.SectionName, jwtSettings);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));

// DK Jwt Generator
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

// Cau hinh Authentication
builder.Services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret)
            )
        }
    );

// DK Hash Password Service
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

// DK UserRepo
builder.Services.AddScoped<IUserRepository, UserRepository>();

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

builder.Services.AddAutoMapper(typeof(MappingProfile));

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
