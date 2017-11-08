using System.Collections.Generic;

namespace Dierenasiel.Data
{
    public class DatabaseInitializer
    {
        public static void Initialize(EntityContext ctx)
        {
            var me = new Owner {FirstName = "Raf", LastName = "Ceuls"};
            ctx.Owners.Add(me);
            ctx.Owners.Add(new Owner {FirstName = "Jos", LastName = "Vermeulen"});
            ctx.Owners.Add(new Owner {FirstName = "Louise", LastName = "Boop"});
            ctx.Animals.Add(new Cat {Name = "Loki", IsMean = false, CurrentOwner = me});
            ctx.Animals.Add(new Cat {Name = "Muffinstorm", IsMean = true, CurrentOwner = me});
            ctx.Animals.Add(new Dog {Name = "Belle", IsADrooler = true, CurrentOwner = null});
            ctx.SaveChanges();
        }
    }
}