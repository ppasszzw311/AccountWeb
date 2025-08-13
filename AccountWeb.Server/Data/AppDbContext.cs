using AccountWeb.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountWeb.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }

}
