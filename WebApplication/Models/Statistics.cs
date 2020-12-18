using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Statistics
    {
        public float AverageAgeValue { get; set; }
        public int AmountOfTrainers { get; set; }
        public int AmountOfClients { get; set; }
        public int AmountOfExtraInfo { get; set; }
        public int AmountOfAlcoholists { get; set; }
        public string LongestInfo { get; set; }
        public int[] GenderPercentage { get; set; }
    }
}
