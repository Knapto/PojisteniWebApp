using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PojisteniWebApp.Models
{
    public enum TypPojisteni
    {
        Životní, Cestovní, Úrazové, Majetkové
    }
    public class Pojistka
    {
        public int Id { get; set; }
        [Display(Name ="Předmět pojištění")]
        public string PredmetPojisteni { get; set; }
        [Display(Name = "Pojištěnec")]
        public int PojistenecId { get; set; }
        [Display(Name = "Typ pojištění")]
        public TypPojisteni? TypPojisteni { get; set; }
        [Display(Name = "Částka")]
        public int Castka { get; set; }
        [Display(Name = "Platnost od")]
        public DateTime PlatnostOd { get; set; }
        [Display(Name = "Platnost do")]
        public DateTime PlatnostDo { get; set; }

        public virtual Pojistenec Pojistenec { get; set; }
    }
}