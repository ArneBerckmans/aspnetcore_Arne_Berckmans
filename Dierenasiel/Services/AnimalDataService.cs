using System;
using System.Collections.Generic;
using System.Linq;
using Dierenasiel.Controllers;
using Dierenasiel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Dierenasiel.Services
{

    /// <summary>
    /// Verantwoordelijk voor het ophalen van data uit de database.
    /// </summary>
    public class AnimalDataService : IAnimalService
    {
        private readonly EntityContext _entityContext;

        public AnimalDataService(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }


        public List<Owner> GetAllOwnersSortedByName()
        {
            return _entityContext.Owners.OrderBy(x => x.FullName).ToList();
        }

        public List<Animal> GetAllAnimalsSortedByName(AnimalCriteria criteria)
        {
            return GetFullyGraphedAnimals()
                .Where(x => string.IsNullOrEmpty(criteria.Name) ||
                            string.Equals(x.Name, criteria.Name, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(x => x.Name).ToList();
        }

        public Animal GetAnimalById(int id)
        {
            return GetFullyGraphedAnimals().FirstOrDefault(x => x.Id == id);
        }

        public Owner GetOwnerbyId(int ownerId)
        {
            return _entityContext.Owners.Find(ownerId);
        }

        private IIncludableQueryable<Animal, Owner> GetFullyGraphedAnimals()
        {
            return _entityContext.Animals.Include(x => x.CurrentOwner);
        }

        public void Persist(Animal animal)
        {
            var obj = animal.Id == 0 ? _entityContext.Add(animal) : _entityContext.Update(animal);
            _entityContext.SaveChanges();
        }
    }
}