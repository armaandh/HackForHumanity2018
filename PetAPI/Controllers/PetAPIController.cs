using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetAPI.Models;

namespace PetAPI.Controllers
{
    [Produces("application/json")]
    //[Route("api/PetAPI")]
    [Route("api/[controller]/[action]")]
    public class PetAPIController:Controller
    {

        public List<LostVM> lostPets = new List<LostVM>()
        {
           new LostVM()
            {
                AnimalType = "Dog",
                DateLost = new DateTime(2018, 1, 9),
                Size = "Large",
                Color = "Black",
                Sex = "Male",
                Age = 4,
                Temperament = "Aggressive"
            },
            new LostVM()
            {
                AnimalType = "Cat",
                DateLost = new DateTime(2018, 2, 9),
                Size = "Small",
                Color = "White",
                Sex = "Female",
                Age = 1,
                Temperament = "Shy"
            },
            new LostVM()
            {
                AnimalType = "Cat",
                DateLost = new DateTime(2017, 12, 9),
                Size = "Small",
                Color = "Brown",
                Sex = "Male",
                Age = 2,
                Temperament = "Aggressive"
            },
            new LostVM()
            {
                AnimalType = "Dog",
                DateLost = new DateTime(2016, 3, 28),
                Size = "Small",
                Color = "Light Brown",
                Sex = "Female",
                Age = 7,
                Temperament = "Shy"
            }

       };
  
        List<FoundVM> foundPets = new List<FoundVM>()
        {
            new FoundVM()
            {
                AnimalType = "Cat",
                DateFound = new DateTime(2016, 10, 9),
                Size = "Medium",
                Color = "White",
                Sex = "Male"
            },
            new FoundVM()
            {
                AnimalType = "Cat",
                DateFound = new DateTime(2017, 5, 19),
                Size = "Medium",
                Color = "Black",
                Sex = "Male"
            },
            new FoundVM()
            {
                AnimalType = "Cat",
                DateFound = new DateTime(2017, 2, 24),
                Size = "Large",
                Color = "White",
                Sex = "Female"
            },
            new FoundVM()
            {
                AnimalType = "Dog",
                DateFound = new DateTime(2017, 8, 10),
                Size = "Large",
                Color = "White",
                Sex = "Male"
            },
            new FoundVM()
            {
                AnimalType = "Dog",
                DateFound = new DateTime(2017, 12, 12),
                Size = "Medium",
                Color = "Black",
                Sex = "Female"
            },
            new FoundVM()
            {
                AnimalType = "Dog",
                DateFound = new DateTime(2017, 7, 31),
                Size = "Medium",
                Color = "Dark Brown",
                Sex = "Female"
            }
        };
            
        [HttpGet]
        public IEnumerable<LostVM> GetAllLostPets()
        {
            return lostPets;
        }

        public IActionResult Index()
        {
            return View(GetAllLostPets());
        }

        [HttpGet]
        public IEnumerable<FoundVM> GetAllFoundPets()
        {
            return foundPets;
        }

        public IActionResult DisplayFoundPets()
        {
            return View(GetAllFoundPets());
        }

        [HttpPost]
        public IActionResult CreateLostPet([FromBody] LostVM model)
        {
            if (ModelState.IsValid)
            {
                LostVM newDog = new LostVM
                {
                    AnimalType = model.AnimalType,
                    DateLost = model.DateLost,
                    Size = model.Size,
                    Color = model.Color,
                    Sex = model.Sex,
                    Age = model.Age,
                    Temperament = model.Temperament
                };
                //Console.WriteLine("lostAnimals size: " + lostAnimals.Count);
                lostPets.Add(newDog);
                //Console.WriteLine("lostAnimals size after add: " + lostAnimals.Count);
                return Ok();
            };
            
            return BadRequest();
        }

        [HttpPost]
        public IActionResult CreateFoundPet([FromBody] FoundVM model)
        {
            if (ModelState.IsValid)
            {
                FoundVM newDog = new FoundVM
                {
                    AnimalType = model.AnimalType,
                    DateFound = model.DateFound,
                    Size = model.Size,
                    Color = model.Color,
                    Sex = model.Sex
                };
                //Console.WriteLine("lostAnimals size: " + lostAnimals.Count);
                foundPets.Add(newDog);
                //Console.WriteLine("lostAnimals size after add: " + lostAnimals.Count);
                return Ok();
            };

            return BadRequest();
        }
    }
}