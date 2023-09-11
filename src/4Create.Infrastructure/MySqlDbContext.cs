using _4Create.Infrastructure.Converters;
using _4Create.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace _4Create.Infrastructure;

public class MySqlDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<SystemLog> SystemLogs { get; set; }
    
    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(c => c.Id);
            
            entity.Property(c => c.Name).IsRequired();
            entity.Property(c => c.CreatedAt).IsRequired();
            entity.Property(c => c.CreatedByUserId).IsRequired();

            entity.HasIndex(c => c.Name)
                  .IsUnique();
            
            entity.HasMany(c => c.Employees)
                .WithMany(e => e.Companies)
                .UsingEntity<CompanyEmployee>(
                    j => j
                        .HasOne(ce => ce.Employee)
                        .WithMany(c => c.CompanyEmployees)
                        .HasForeignKey(ce => ce.EmployeeId),
                    j => j
                        .HasOne(ce => ce.Company)
                        .WithMany(e => e.CompanyEmployees)
                        .HasForeignKey(ce => ce.CompanyId),
                    j =>
                    {
                        j.HasKey(ce => new { ce.EmployeeId, ce.CompanyId });
                    });
        });
        
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.CreatedByUserId).IsRequired();

            entity.HasIndex(e => e.Email)
                  .IsUnique();
            
            entity.ToTable(tb => tb.HasTrigger("fakeTrigger"));
        });
        
        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.HasKey(l => l.Id);
            
            entity.Property(l => l.ResourceType).IsRequired();
            entity.Property(l => l.ResourceId).IsRequired();
            entity.Property(l => l.CreatedAt).IsRequired();
            entity.Property(l => l.EventName).IsRequired();
            entity.Property(l => l.Comment).IsRequired();
            entity.Property(l => l.ChangeSet)
                .IsRequired()
                .HasConversion<ObjectToJsonValueConverter>();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Username).IsRequired();
            entity.Property(u => u.HashedPassword).IsRequired();
            entity.Property(u => u.Role).IsRequired();

            entity.HasData(DbContextSeedingData.GetUsers());

            entity.HasIndex(u => u.Username).IsUnique();
        });
    }
}
