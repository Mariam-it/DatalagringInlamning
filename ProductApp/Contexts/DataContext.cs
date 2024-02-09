using Microsoft.EntityFrameworkCore;
using ProductApp.Entities;

namespace ProductApp.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public virtual DbSet<RoleEntity> Roles { get; set; }
    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<ProfileEntity> Profiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>()
            .HasIndex(x => x.RoleName)
            .IsUnique();

        modelBuilder.Entity<UserEntity>()
        .HasIndex(x => x.Email)
        .IsUnique();
    }
}
