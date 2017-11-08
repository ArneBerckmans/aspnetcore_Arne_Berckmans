using System.ComponentModel.DataAnnotations;

namespace Dierenasiel.Data
{
    public class Cat : Animal
    {
        [Required]
        public bool IsMean { get; set; }
    }
}