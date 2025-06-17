using ContactsApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Api.Data;

public class ContactsDbContext : DbContext
{
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options): base(options)
    {
    }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}