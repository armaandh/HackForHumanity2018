using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace PetBot.Services
{
    public class PetService
    {
        public static async Task<List<Dog>> GetFoundDogs()
        {
            string uri = "http://petapihackforhumanity2018.azurewebsites.net/api/PetAPI/GetAllFoundPets";
            using (var client = new WebClient())
            {
                var rawData = await client.DownloadStringTaskAsync(new Uri(uri));
                var dogData = JsonConvert.DeserializeObject<List<Dog>>(rawData);
                return dogData;
            }
            return null;
        }
    }
}