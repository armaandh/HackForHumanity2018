using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetAPI.Models
{
    public class LostVM
    {
        public string AnimalType { get; set; }
        public DateTime DateLost { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Temperament { get; set; }
    }
}
