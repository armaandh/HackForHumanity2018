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
    [Route("api/PetAPI")]
    public class PetAPIController : Controller
    {
        public List<LostVM> GetSampleData()
        {
            List<LostVM> lostAnimals = new List<LostVM>();
            lostAnimals.Add(new LostVM()
            {
                AnimalType = "Dog",
                DateLost = new DateTime(2018, 1, 9),
                Size = "Large",
                Color = "Black",
                Sex = "Male",
                Age = 4,
                Temperament = "Aggressive"
            });
            lostAnimals.Add(new LostVM()
            {
                AnimalType = "Cat",
                DateLost = new DateTime(2018, 2, 9),
                Size = "Small",
                Color = "White",
                Sex = "Female",
                Age = 0.5,
                Temperament = "Shy"
            });
            lostAnimals.Add(new LostVM()
            {
                AnimalType = "Cat",
                DateLost = new DateTime(2017, 12, 9),
                Size = "Small",
                Color = "Brown",
                Sex = "Male",
                Age = 2,
                Temperament = "Aggressive"
            });
            lostAnimals.Add(new LostVM()
            {
                AnimalType = "Dog",
                DateLost = new DateTime(2016, 3, 28),
                Size = "Small",
                Color = "Light Brown",
                Sex = "Female",
                Age = 7,
                Temperament = "Shy"
            });

            return lostAnimals;
        }

        [HttpGet]
        public IEnumerable<LostVM> GetAll()
        {
            return GetSampleData();
        }

        public IActionResult Index()
        {

            return View(GetAll());
        }
    }
}