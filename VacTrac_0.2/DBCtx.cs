using System;
using VacTrac.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data.Common;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Reflection;

namespace VacTrac
{
    public class DBCtx: DbContext
    {
        public DbSet<Vaccines> Vaccines { get; set; }
        public DbSet<Vaccines> InventoryCount { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "Database\\VacTracDB.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

    }
}
