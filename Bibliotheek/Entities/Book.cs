using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotheek.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual List<AuthorBook> Authors { get; set; }
    }
}
