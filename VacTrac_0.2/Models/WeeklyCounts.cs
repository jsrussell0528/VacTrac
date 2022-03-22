using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacTrac.Models
{
    public class WeeklyCounts
    {
        public int ID { get; set; }
        public int VaccineID { get; set; }
        public string Date { get; set; }
        public int Count { get; set; }
    }
}
