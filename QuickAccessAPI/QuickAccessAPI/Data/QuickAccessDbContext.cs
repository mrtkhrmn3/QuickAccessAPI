using Microsoft.EntityFrameworkCore;
using QuickAccessAPI.Entities;

public class QuickAccessDbContext : DbContext
{
    public QuickAccessDbContext(DbContextOptions<QuickAccessDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<SiteManager> SiteManagers { get; set; }
    public DbSet<Resident> Residents { get; set; }
    public DbSet<Security> Securities { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User ile roller arası birebir ilişkiler
        modelBuilder.Entity<Admin>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Admin>(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SiteManager>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<SiteManager>(sm => sm.Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Resident>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Resident>(r => r.Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Security>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Security>(s => s.Id)
            .OnDelete(DeleteBehavior.Cascade);

        // Notification -> User ilişki (bildirimi oluşturan kullanıcı)
        modelBuilder.Entity<Notification>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // AptNo'yu int yapacaksan burayı güncelle (şu an string olarak kalıyor)
    }
}
