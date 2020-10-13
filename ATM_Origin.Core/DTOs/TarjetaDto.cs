using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Origin.Core.DTOs
{
    public class TarjetaDto
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public DateTime? FechaVto { get; set; }
        public string Pin { get; set; }
        public decimal Balance { get; set; }
        public bool Habilitada { get; set; }
    }
}
