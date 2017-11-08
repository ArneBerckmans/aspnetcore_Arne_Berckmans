using Microsoft.EntityFrameworkCore;

namespace Dierenasiel.Data
{
    /// <summary>
    /// Via deze klasse gaat alle interactie met de database. Deze versie is de bare-bones implementatie. 
    /// </summary>
    public class EntityContext : DbContext
    {
        /// <summary>
        /// De constructor die wordt aangeroepen bij het instantieren van de database context. Deze wordt aangeroepen op het moment dat
        /// de dependency injection container een instantie van EntityContext instantieert.
        /// </summary>
        /// <param name="options">Configuratie-opties</param>
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }

        /// <summary>
        /// Als je hier een lijst zou opvragen, zou je een "select * from Animal" doen, een insert zou "insert into ... " doen, etc
        /// </summary>
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}