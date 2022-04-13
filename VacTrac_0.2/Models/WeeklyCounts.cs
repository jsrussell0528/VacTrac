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

        [Required(ErrorMessage = "Date is required.")]
        [RegularExpression(@"((0[1-9]|1[0-2])\/((0|1)[0-9]|2[0-9]|3[0-1])\/((19|20)\d\d))", ErrorMessage = "Invalid date format. MM/DD/YYYY")]
        public string Date { get; set; }

        [Required(ErrorMessage = "Acuuvax Count is required.")]
        public int? AccuVaxCount { get; set; }

        [Required(ErrorMessage = "Fridge Count is required.")]
        public int? FridgeCount { get; set; }

        //[Required(ErrorMessage = "Dispensed Count is required.")]
        //public int? Dispensed { get; set; }
        [Required]
        public int VaccinesID { get; set; }
    }
}
