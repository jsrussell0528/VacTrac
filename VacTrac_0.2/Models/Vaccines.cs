using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacTrac.Models
{
    public class Vaccines
    {
        public int ID { get; set; }
        public string VaccineName { get; set; }
        public string? Description { get; set; }
        public string? MonthlyPAR { get; set; }
        public string? WeeklyPAR { get; set; }
        public bool? Private { get; set; }
        public int? InventoryTotal { get; set; }
    }
}
