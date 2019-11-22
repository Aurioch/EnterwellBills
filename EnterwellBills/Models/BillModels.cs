using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace EnterwellBills.Models
{
    public class Faktura
    {
        public int Id { get; set; }
        public DateTime DatumStvaranja { get; set; }
        public DateTime DatumDospijeca { get; set; }
        public virtual List<Stavka> Stavke { get; set; }
        public double Cijena { get; set; }
        public string PDV { get; set; }
        public double CijenaSaPDV { get; set; }
        public string Stvaratelj { get; set; }
        public string Primatelj { get; set; }
    }

    public class Stavka
    {
        public int Id { get; set; }
        public string Opis { get; set; }
        public int Kolicina { get; set; }
        public double JedinicnaCijena { get; set; } // bez poreza
        public double UkupnaCijena { get; set; } // bez poreza

        public virtual Faktura Faktura { get; set; }
    }
}