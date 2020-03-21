using Microsoft.EntityFrameworkCore;
using CANAdmin.Shared.Models;

namespace CANAdmin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CANDatabase> CANDatabases { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Signal> Signals { get; set; }
        public DbSet<NetworkNode> NetworkNodes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CANDatabase>()
                .Property(c => c.Date)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Message>()
                .HasOne(m => m.CANDatabase)
                .WithMany(c => c.Messages)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Signal>()
                .HasOne(s => s.Message)
                .WithMany(m => m.Signals)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NetworkNode>()
                .HasOne(n => n.CANDatabase)
                .WithMany(c => c.NetworkNodes)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
