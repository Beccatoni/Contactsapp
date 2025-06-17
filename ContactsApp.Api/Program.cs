using ContactsApp.Api.Data;
using ContactsApp.Api.Models;
using ContactsApp.Api.Repositories;
using ContactsApp.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Adding services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register our repositories and services
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();


builder.Services.AddDbContext<ContactsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ContactsDb")));


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Swagger documentaition
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseMiddleware<ContactsApp.Api.Middlewares.ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


