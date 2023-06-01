using ConsoleToWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleToWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private List<AnimalModel>? animals = null;
        public AnimalsController()
        {
            animals = new List<AnimalModel>()
            {
                new AnimalModel() { Id = 1, Name = "Himanshu"},
                new AnimalModel() { Id = 2, Name = "Ladva"}
            };
        }

        [Route("", Name = "Home")]
        public IActionResult GetAnimals()
        {
            return Ok(animals);
        }

        [Route("test")]
        public IActionResult GetAnimalsTest()
        {
            //return Accepted(animals);
            //return AcceptedAtAction("GetAnimals");
            // return AcceptedAtRoute("Home");
            return LocalRedirect("~/api/animals");
        }

        [Route("{name}")]
        public IActionResult GetAnimalsByName(string name)
        {
            if (!name.Contains("abc"))
            {
                return BadRequest();
            }
            return Ok(animals);
        }

        [Route("{id:int}")]
        public IActionResult GetAnimalsById(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var animal = animals?.FirstOrDefault(x => x.Id == id);
            if(animal == null) 
            {
                return NotFound();
            }
            return Ok(animal);
        }

        [HttpPost("")]
        public IActionResult GetAnimals(AnimalModel animal)
        {
            animals?.Add(animal);
            // return Created("~/api/animals/"+animal.Id,animal); 
            return CreatedAtAction("GetAnimalsById", new { id = animal.Id}, animal);
        }
    }
}
