using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bibliotheek.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bibliotheek.Data
{
    public class DatabaseInitializer
    {
        public static void InitializeDatabase(EntityContext entityContext)
        {
            if (((entityContext.GetService<IDatabaseCreator>() as RelationalDatabaseCreator)?.Exists()).GetValueOrDefault(false))
            {
                return;
            }
            var authors = new List<Author>();
            for (var i = 0; i < 20; i++)
            {
                authors.Add(new Author {FirstName = $"Author First Name {i}", LastName = $"Author Last Name {i}"});
            }

            var books = new List<Book>();
            for (var i = 0; i < 20; i++)
            {
                var authorBook = new AuthorBook()
                {
                    Author = authors[i]
                };
                books.Add(new Book {Title = $"Book {i}", Authors = new List<AuthorBook> {authorBook}});
            }

            var me = new Author {FirstName = "Raf", LastName = "Ceuls"};
            books[0].Authors.Add(new AuthorBook() { Author = me});

            entityContext.Database.EnsureCreated();
            entityContext.Authors.AddRange(authors);
            entityContext.Books.AddRange(books);
            entityContext.SaveChanges();
        }
    }
}