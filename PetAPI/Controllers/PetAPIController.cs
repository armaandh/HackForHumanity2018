﻿using System;
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
    public class PetAPIController : Controller
    {
        public List<LostVM> GetSampleLostData()
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

        public List<FoundVM> GetSampleFoundData()
        {
            List<FoundVM> foundAnimals = new List<FoundVM>();
            foundAnimals.Add(new FoundVM()
            {
                AnimalType = "Cat",
                DateFound = new DateTime(2016, 10, 9),
                Size = "Medium",
                Color = "White",
                Sex = "Male"
            });
            foundAnimals.Add(new FoundVM()
            {
                AnimalType = "Cat",
                DateFound = new DateTime(2017, 5, 19),
                Size = "Medium",
                Color = "Black",
                Sex = "Male"
            });
            foundAnimals.Add(new FoundVM()
            {
                AnimalType = "Cat",
                DateFound = new DateTime(2017, 2, 24),
                Size = "Large",
                Color = "White",
                Sex = "Female"
            });
            foundAnimals.Add(new FoundVM()
            {
                AnimalType = "Dog",
                DateFound = new DateTime(2017, 8, 10),
                Size = "Large",
                Color = "White",
                Sex = "Male"
            });
            foundAnimals.Add(new FoundVM()
            {
                AnimalType = "Dog",
                DateFound = new DateTime(2017, 12, 12),
                Size = "Medium",
                Color = "Black",
                Sex = "Female"
            });
            foundAnimals.Add(new FoundVM()
            {
                AnimalType = "Dog",
                DateFound = new DateTime(2017, 7, 31),
                Size = "Medium",
                Color = "Dark Brown",
                Sex = "Female"
            });


            return foundAnimals;
        }

        [HttpGet]
        public IEnumerable<LostVM> GetAllLost()
        {
            return GetSampleLostData();
        }

        public IActionResult Index()
        {

            return View(GetAllLost());
        }

        
        [HttpGet]
        public IEnumerable<FoundVM> GetAllFound()
        {
            return GetSampleFoundData();
        }

        public IActionResult DisplayFound()
        {

            return View(GetAllFound());
        }

        [HttpPost]
        public IActionResult CreateLostPet(LostVM model)
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

                GetSampleLostData().Add(newDog);
                return Ok();

            };
            
            return BadRequest();
        }
    }
}