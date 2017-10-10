using Bibliotheek.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bibliotheek.Data
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasKey(b => b.Id);
            modelBuilder.Entity<Book>().HasMany(b => b.Authors).WithOne(ba => ba.Book);

            modelBuilder.Entity<AuthorBook>().HasKey(ab => new {ab.AuthorId, ab.BookId});

            modelBuilder.Entity<Author>().HasKey(a => a.Id);
            modelBuilder.Entity<Author>().HasMany(b => b.Books).WithOne(ba => ba.Author);

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
