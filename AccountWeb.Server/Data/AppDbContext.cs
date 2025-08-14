using AccountWeb.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountWeb.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    // DbSets for each model
    public DbSet<User> Users { get; set; }
    public DbSet<Factory> Factories { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<UserGroupMapping> UserGroupMappings { get; set; }
    public DbSet<DomainCategory> DomainCategories { get; set; }
    public DbSet<DomainTypes> DomainTypes { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }
    public DbSet<LoginRecord> LoginRecords { get; set; }
    public DbSet<AccountActionRecord> AccountActionRecords { get; set; }
}
