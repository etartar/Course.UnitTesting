using Microsoft.EntityFrameworkCore;
using UnderstandingDependencies.Api.Models;

namespace UnderstandingDependencies.Api.Context;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-86NC1DJ;Initial Catalog=UnitTestDB;Integrated Security=True;Trust Server Certificate=True");
    }

    public DbSet<AppUser> Users { get; set; }
}
