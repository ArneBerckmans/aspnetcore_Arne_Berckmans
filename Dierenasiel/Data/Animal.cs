namespace Dierenasiel.Data
{
    public class Animal : BaseDbEntity
    {
        public string Name { get; set; }
        public Owner CurrentOwner { get; set; }
    }
}