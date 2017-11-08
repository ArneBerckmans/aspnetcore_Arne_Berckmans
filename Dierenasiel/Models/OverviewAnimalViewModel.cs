using Dierenasiel.Data;

namespace Dierenasiel.Models
{
    public class OverviewAnimalViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AnimalKind { get; set; }
        public bool HasBeenPlaced { get; set; }
        public string ExtraInformation { get; set; }
    }
}
