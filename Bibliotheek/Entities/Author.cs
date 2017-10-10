using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bibliotheek.Entities
{
    public class Author
    {
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        // OR public string FullName => $ "{FirstName} {LastName}";

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<AuthorBook> Books { get; set; }
    }
}