using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dierenasiel.Models
{
    public class EditAnimalViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> PotentialOwners { get; set; }
        public string Owner { get; set; }
    }
}