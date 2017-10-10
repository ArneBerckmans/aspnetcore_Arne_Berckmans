using System;
using System.Collections.Generic;

namespace Bibliotheek.Models
{
    public class BookListViewModel
    {
        public List<BookDetailViewModel> Books { get; set; }
        public DateTime GeneratedAt => DateTime.Now;
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime CreationDate { get; set; }
        public int Id { get; set; }
    }
}