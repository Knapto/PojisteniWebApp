using PojisteniWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PojisteniWebApp.DAL
{
    public class PojisteniWebIni : System.Data.Entity.DropCreateDatabaseIfModelChanges<PojisteniWebContext>
    {
        protected override void Seed(PojisteniWebContext context)
        {
            var pojistenci = new List<Pojistenec>
            {
                new Pojistenec{ Jmeno = "Tomáš", Prijmeni = "Knap", Email = "Tomasknap007@gmail.com", Heslo = "Heslo456!", DatumNarozeni = DateTime.Parse("1998.10.02"), TelCislo = 792518250, Ulice = "Dvorce", Mesto = "Lysá nad Labem", Psc = 28922 },
                new Pojistenec{ Jmeno = "Petr", Prijmeni = "Pan", Email = "Petrpan@email.cz", Heslo = "kherwbg654?!!", DatumNarozeni = DateTime.Parse("1978.06.06"), TelCislo = 606528745, Ulice = "Dukelská", Mesto = "Milovice", Psc = 28924 },
                new Pojistenec{ Jmeno = "Jana", Prijmeni = "Novotná", Email = "JanaNovotna@gmail.com", Heslo = "hbJIUHFss22395_!", DatumNarozeni = DateTime.Parse("1989.12.24"), TelCislo = 702694381, Ulice = "ČSA", Mesto = "Lysá nad Labem", Psc = 28922 }
            };

                pojistenci.ForEach(s => context.Pojistenci.Add(s));
                context.SaveChanges();
        }
    }
    
}