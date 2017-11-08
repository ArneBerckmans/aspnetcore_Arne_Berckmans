using Microsoft.AspNetCore.Mvc;
using Dierenasiel.Models;
using Dierenasiel.Services;

namespace Dierenasiel.Controllers
{
    /// <summary>
    /// Merk op dat hier geen "Route"-attribute staat. Dit wilt zeggen dat hij de 'default route' (uit appsettings.cs) gaat respecteren.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ITranslationService _translationService;

        public HomeController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            var model = _translationService.GetAllAnimalViewModels(new AnimalCriteria());
            return View(model);
        }

        [HttpGet("{id}")]
        public IActionResult Detail([FromRoute] int id)
        {
            var model = _translationService.GetEditAnimalViewModel(id);
            if (model is EditCatViewModel viewModel)
            {
                return View("EditCat", viewModel);
            }
            if (model is EditDogViewModel dogViewModel)
            {
                return View("EditDog", dogViewModel);
            }
            //Als je géén hond en géén kat hebt, een 404. 
            return NotFound();

        }


        [HttpGet]
        public IActionResult Create(AnimalType type)
        {
            switch (type)
            {
                case AnimalType.Cat:
                    return View("EditCat", new EditCatViewModel()
                    {
                        PotentialOwners = _translationService.GetOwnersAsSelectListItems()
                    });
                case AnimalType.Dog:
                    return View("EditDog", new EditDogViewModel()
                    {
                        PotentialOwners = _translationService.GetOwnersAsSelectListItems()
                    });
            }
            // valt te onderhandelen of dit geen 5XX moet zijn.
            return NotFound();
        }


        [HttpPost]
        public IActionResult PersistCat([FromForm] EditCatViewModel animal)
        {
            _translationService.PersistCat(animal);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult PersistDog([FromForm] EditDogViewModel animal)
        {
            _translationService.PersistDog(animal);
            return RedirectToAction(nameof(Index));
        }

    }
}
