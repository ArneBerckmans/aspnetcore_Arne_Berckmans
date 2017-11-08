using System.Collections.Generic;
using Dierenasiel.Controllers;
using Dierenasiel.Data;

namespace Dierenasiel.Services
{
    public interface IAnimalService
    {
        List<Owner> GetAllOwnersSortedByName();
        List<Animal> GetAllAnimalsSortedByName(AnimalCriteria criteria);
        Animal GetAnimalById(int id);
        Owner GetOwnerbyId(int ownerId);
        void Persist(Animal persist);
    }
}