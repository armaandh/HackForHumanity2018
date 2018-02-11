using Microsoft.Bot.Builder.FormFlow;
using System;

namespace PetBot
{
    [Serializable]
    public class FoundQuery
    {
        [Prompt("When is the animal found?")]
        public DateTime DateFound { get; set; }
        [Prompt("What type of animal is it? (eg. cat/dog)")]
        public string Type { get; set; }
        [Prompt("What size is the animal?")]
        public string Size { get; set; }
        [Prompt("What color(s) is the animal?")]
        public string Color { get; set; }
        [Prompt("What sex is the animal?")]
        public string Sex { get; set; }
        [Prompt("What age is the animal?")]
        public string Age { get; set; }
        [Prompt("Any other info about the animal?")]
        public string Other { get; set; }

    }
}