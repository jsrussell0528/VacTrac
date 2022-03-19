using System;
using VacTrac.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacTrac
{
    public class DBCtx: DbContext
    {
        public DBCtx(DbContextOptions<DBCtx> options) : base(options)
        {
        }

        public DbSet<Vaccine> Vaccines { get; set; }
    }
}
