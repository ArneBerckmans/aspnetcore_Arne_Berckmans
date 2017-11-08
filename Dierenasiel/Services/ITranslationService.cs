using System.Collections.Generic;
using Dierenasiel.Controllers;
using Dierenasiel.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dierenasiel.Services
{
    public interface ITranslationService
    {
        List<OverviewAnimalViewModel> GetAllAnimalViewModels(AnimalCriteria criteria);
        EditAnimalViewModel GetEditAnimalViewModel(int id);
        void PersistCat(EditCatViewModel animal);
        void PersistDog(EditDogViewModel animal);
        List<SelectListItem> GetOwnersAsSelectListItems();
    }
}