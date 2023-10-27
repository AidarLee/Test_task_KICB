using System;
using System.Collections.Generic;
using CardsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CardsApi
{
    public partial class Test_taskContext : DbContext
    {
        public Test_taskContext()
        {
        }

        public Test_taskContext(DbContextOptions<Test_taskContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=FRIDAY;Database=Test_task;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CardName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Card_name");

                entity.HasOne(d => d.CurrencyNavigation)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.Currency)
                    .HasConstraintName("FK_Cards_Currency");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(85)
                    .HasColumnName("First_Name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(85)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.PhoneNumber).HasColumnName("Phone_Number");

                entity.HasOne(d => d.CardNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.Card)
                    .HasConstraintName("FK_Clients_Cards");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrencyName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Currency_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
