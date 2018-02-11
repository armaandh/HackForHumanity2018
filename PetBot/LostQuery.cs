namespace PetBot
{
    using System;
    using Microsoft.Bot.Builder.FormFlow;

    [Serializable]
    public class LostQuery
    {

        [Numeric(1, int.MaxValue)]
        [Prompt("How many {&} days back did you lose your dog?")]
        public int Nights { get; set; }
    }
}