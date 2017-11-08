using System.Collections.Generic;
using System.Linq;
using Dierenasiel.Controllers;
using Dierenasiel.Data;
using Dierenasiel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dierenasiel.Services
{
    /// <summary>
    /// Verantwoordelijk voor het omzetten van de data uit de database naar viewmodels, en omgekeerd.
    /// </summary>
    public class TranslationService : ITranslationService
    {
        private readonly IAnimalService _animalService;

        public TranslationService(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        public List<SelectListItem> GetOwnersAsSelectListItems()
        {
            var owners = _animalService.GetAllOwnersSortedByName().Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString()
            }).ToList();
            owners.Insert(0, new SelectListItem()
            {
                Text = "Niemand",
                Value = "0"
            });
            return owners;
        }

        public List<OverviewAnimalViewModel> GetAllAnimalViewModels(AnimalCriteria criteria)
        {
            // Include doet een "eager load". Dit kan je het beste bekijken als een join over een gerelateerde table.
            return _animalService.GetAllAnimalsSortedByName(new AnimalCriteria())
                .Select(x => CreateOverviewAnimalViewModel(x))
                .OrderBy(x => x.Name).ToList();
        }

        public EditAnimalViewModel GetEditAnimalViewModel(int id)
        {
            var data = _animalService.GetAnimalById(id);
            switch (data)
            {
                case Cat cat:
                    return new EditCatViewModel
                    {
                        Id = data.Id,
                        Name = data.Name,
                        IsMean = cat.IsMean,
                        PotentialOwners = GetOwnersAsSelectListItems(),
                        Owner = $"{data.CurrentOwner?.Id}"
                    };
                case Dog dog:
                    return new EditDogViewModel
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Drools = dog.IsADrooler,
                        PotentialOwners = GetOwnersAsSelectListItems(),
                        Owner = $"{data.CurrentOwner?.Id}"
                    };
                default:
                    return null;
            }
        }


        public void PersistCat(EditCatViewModel cat)
        {
            var data = _animalService.GetAnimalById(cat.Id) as Cat ?? new Cat();
            data.IsMean = cat.IsMean;
            data.Name = cat.Name;
            AssignOwner(cat.Owner, data);
            _animalService.Persist(data);
        }

        private void AssignOwner(string ownerId, Animal data)
        {
            data.CurrentOwner = !string.IsNullOrEmpty(ownerId) ? _animalService.GetOwnerbyId(int.Parse(ownerId)) : null;
        }

        public void PersistDog(EditDogViewModel dog)
        {
            var data = _animalService.GetAnimalById(dog.Id) as Dog ?? new Dog();
            data.IsADrooler = dog.Drools;
            data.Name = dog.Name;
            AssignOwner(dog.Owner, data);
            _animalService.Persist(data);
        }

        private static OverviewAnimalViewModel CreateOverviewAnimalViewModel(Animal x)
        {
            return new OverviewAnimalViewModel
            {
                Id = x.Id,
                Name = x.Name,
                AnimalKind = x.GetType().Name,
                HasBeenPlaced = x.CurrentOwner != null,
                ExtraInformation = x is Cat ? ((Cat) x).IsMean.ToString() : ((Dog) x).IsADrooler.ToString()
            };
        }
    }
}