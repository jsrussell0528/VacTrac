using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacTrac.Models
{
    public class InventoryCount
    {
        public int ID { get; set; }
        public string VaccineID { get; set; }
        public bool IsPrivate { get; set; }
        public int Count { get; set; }
    }
}
