using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Origin.Core.DTOs
{
    public class OperacionesDto
    {
        public int Id { get; set; }
        public int TarjetaId { get; set; }
        public DateTime? Fecha { get; set; }
        public string CodigoOperacion { get; set; }
        public decimal? Balance { get; set; }

    }
}
