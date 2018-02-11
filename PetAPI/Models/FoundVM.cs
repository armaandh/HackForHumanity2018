using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetAPI.Models
{
    public class FoundVM
    {
        public string AnimalType { get; set; }
        public DateTime DateFound { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Sex { get; set; }
    }
}
