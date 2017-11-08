namespace Dierenasiel.Data
{
    public class Owner : BaseDbEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{LastName} {FirstName}";
    }
}