using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PojisteniWebApp.Models;

namespace PojisteniWebApp.DAL
{
    public class PojisteniWebContext : DbContext
    {
        public PojisteniWebContext() : base("PojisteniWebContext") 
        {
        }

        public DbSet<Pojistenec> Pojistenci { get; set; }
        public DbSet<Pojistka> Pojistky { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}