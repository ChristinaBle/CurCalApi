using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CurCalApi.Models
{
    public partial class CurrencyCalculatorContext : DbContext
    {
        public CurrencyCalculatorContext(DbContextOptions<CurrencyCalculatorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExchangeRates> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExchangeRates>(entity =>
            {
                entity.Property(e => e.BasicCurrency)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeRate).HasColumnType("decimal(9, 4)");

                entity.Property(e => e.TargetCurrency)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
