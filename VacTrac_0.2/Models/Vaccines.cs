﻿using System;
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
        public string VaccineName { get; set; }
        public string? Description { get; set; }
        public int? MonthlyPAR { get; set; }
        public int? WeeklyPAR { get; set; }
        public string? Private { get; set; }
    }
}
