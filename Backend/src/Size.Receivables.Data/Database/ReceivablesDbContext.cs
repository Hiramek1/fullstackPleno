using Microsoft.EntityFrameworkCore;
using Size.Receivables.Domain.Entities;
using Size.Receivables.Domain.ValueObjects;

namespace Size.Receivables.Data.Database;

public class ReceivablesDbContext : DbContext
{
    public ReceivablesDbContext(DbContextOptions<ReceivablesDbContext> options) : base(options)
    { }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<CartItem> CartItems => Set<CartItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasMany(e => e.Invoices)
                  .WithOne()
                  .HasForeignKey(i => i.CompanyId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.Property(i => i.Number).IsRequired();
            entity.Property(i => i.DueDate).IsRequired();
            entity.Property(i => i.CompanyId).IsRequired();
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.Property(i => i.InvoiceId).IsRequired();
            entity.Property(i => i.CompanyId).IsRequired();
        });
    }
}
