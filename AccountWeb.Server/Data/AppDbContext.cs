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
    public DbSet<DomainType> DomainTypes { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }
    public DbSet<LoginRecord> LoginRecords { get; set; }
    public DbSet<AccountActionRecord> AccountActionRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 定義模型之間的關係和約束
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.UserId).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasOne(d => d.Factory)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.FactoryId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Factory>(entity =>
        {
            entity.HasIndex(e => e.FactoryId).IsUnique();
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasIndex(e => e.ProjectId).IsUnique();
            entity.HasOne(d => d.Factory)
                .WithMany(p => p.Projects)
                .HasForeignKey(d => d.FactoryId)
                .OnDelete(DeleteBehavior.Cascade); // 刪除時會一併刪除相依資料
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasIndex(e => e.RoleName).IsUnique();
        });

        modelBuilder.Entity<UserRoleMapping>(entity =>
        {
            entity.HasOne(d => d.User)
                .WithMany(p => p.UserRoleMappings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(d => d.Role)
                .WithMany(p => p.UserRoleMappings)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasOne(d => d.Project)
                .WithMany(p => p.Groups)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserGroupMapping>(entity =>
        {
            entity.HasOne(d => d.User)
                .WithMany(p => p.UserGroupMappings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(d => d.Group)
                .WithMany(p => p.UserGroupMappings)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DomainType>(entity =>
        {
            entity.HasOne(d => d.Category)
                .WithMany(p => p.DomainTypes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasOne(d => d.DomainType)
                .WithMany(p => p.Accounts)
                .HasForeignKey(d => d.OsType)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(d => d.Project)
                .WithMany(p => p.Accounts)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProjectMember>(entity =>
        {
            entity.HasOne(d => d.User)
                .WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(d => d.Project)
                .WithMany(p => p.ProjectMembers)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<LoginRecord>(entity =>
        {
            entity.HasOne(d => d.User)
                .WithMany(p => p.LoginRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AccountActionRecord>(entity =>
        {
            entity.HasOne(d => d.User)
                .WithMany(p => p.AccountActionRecords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(d => d.Account)
                .WithMany(p => p.AccountActionRecords)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        });

    }
}
