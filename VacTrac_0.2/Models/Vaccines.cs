using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VacTrac.Models
{
    public class Vaccines
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Vaccine Name is required.")]
        public string VaccineName { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Monthly Par is required.")]
        public int? MonthlyPAR { get; set; }
        [Required(ErrorMessage = "Weekly Par is required.")]
        public int? WeeklyPAR { get; set; }
        [Required(ErrorMessage = "Private or VFC Status is required.")]
        public string? Private { get; set; }
    }
}
