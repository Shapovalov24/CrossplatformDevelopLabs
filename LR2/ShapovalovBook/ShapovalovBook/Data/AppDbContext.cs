using Microsoft.EntityFrameworkCore;
using ShapovalovBook.Models;

namespace ShapovalovBook.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
                entity.Property(b => b.Year).IsRequired();

                entity.HasOne<Author>()
                      .WithMany(a => a.Books)
                      .HasForeignKey(b => b.AuthorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
