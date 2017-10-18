using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetDataFromBank.Models
{
    public class MovementViewModel
    {
        public DateTime FechaOperacion { get; set; }
        public DateTime FechaValor { get; set; }
        public string Concepto { get; set; }
        public float Importe { get; set; }
        public float Saldo { get; set; }

    }
}