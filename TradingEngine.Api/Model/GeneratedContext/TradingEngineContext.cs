using Microsoft.EntityFrameworkCore;

namespace TradingEngine.Api.Model.GeneratedContext
{
    public partial class TradingEngineContext : DbContext
    {
        public TradingEngineContext()
        {
        }

        public TradingEngineContext(DbContextOptions<TradingEngineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserBalance> UserBalance { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(local)\\SQLExpress;Initial Catalog=TradingEngine;Integrated Security=True;MultipleActiveResultSets=true;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ratio).HasColumnType("decimal(16, 2)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserBalance>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(16, 2)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBalance)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Balance_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
