using EastWestTest.Repository.Abstract;
using EastWestTest.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace EastWestTest.Repository.Context
{
    public class DataContext : DbContext, IUnitOfWork
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<SaleEntity> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SaleEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.CreateDate);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ClientId);
            });

            builder.Entity<ClientEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
