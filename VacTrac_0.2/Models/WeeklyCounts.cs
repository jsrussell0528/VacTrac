using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VacTrac.Models
{
    public class WeeklyCounts
    {
        [Key]
        public int ID { get; set; }
        public string Date { get; set; }
        public int? AccuVaxCount { get; set; }
        public int? FridgeCount { get; set; }
        public int? Dispensed { get; set; }
        public int VaccinesID { get; set; }
    }
}
