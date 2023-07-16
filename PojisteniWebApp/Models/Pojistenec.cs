using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PojisteniWebApp.Models
{
    public class Pojistenec
    {
        public int Id { get; set; }
        [Display(Name ="Jméno")]
        public string Jmeno { get; set; }
        [Display(Name = "Příjmení")]
        public string Prijmeni { get; set; }
        public string Email { get; set; }
        public string Heslo { get; set; }
        [Display(Name = "Datum narození")]
        public DateTime DatumNarozeni { get; set; }
        [Display(Name = "Telefonní číslo")]
        public int TelCislo { get; set; }
        public string Ulice { get; set; }
        [Display(Name = "Město")]
        public string Mesto { get; set; }
        [Display(Name = "Psč")]
        public int Psc { get; set; }
        public virtual ICollection<Pojistka> Pojistky { get; set; }
    }
}