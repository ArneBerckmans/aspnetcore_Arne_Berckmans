using Dierenasiel.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dierenasiel.Controllers
{
    /// <summary>
    /// "By Convention" is het de bedoeling dat je je "API" methodes op de route "API" laat plaatsnemen.
    /// </summary>
    [Route("/api/")]
    public class AnimalApiController : Controller
    {
        private readonly ITranslationService _animalService;

        /// <summary>
        /// Vermits we de AnimalApiController ook aanmaken via het dependency injection mechanisme, kunnen we hier van constructor injection gebruik maken.
        /// Dit wil zeggen dat de container slim genoeg is om te weten dat hij een class moet initialiseren dat je in je startup gedefinieerd hebt.
        /// </summary>
        /// <param name="animalService"></param>
        public AnimalApiController(ITranslationService animalService)
        {
            _animalService = animalService;
        }
        
        [HttpGet("animals")]
        public IActionResult GetAllAnimals()
        {
            // Een "OK(object)" result gaat een JSON-result teruggeven.
            return Ok(_animalService.GetAllAnimalViewModels(new AnimalCriteria()));
        }

        [HttpGet("animals/{id}")]
        public IActionResult GetAnimalById([FromRoute] int id)
        {
            var animal = _animalService.GetEditAnimalViewModel(id);
            if (animal == null)
            {
                return NotFound();
            }
            return Ok(animal);
        }

        [HttpGet("animals/search")]
        public IActionResult Search([FromQuery] AnimalCriteria criteria)
        {
            return Ok(_animalService.GetAllAnimalViewModels(criteria));
        }

    }
}