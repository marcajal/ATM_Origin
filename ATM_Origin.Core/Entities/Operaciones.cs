using System;
using System.Collections.Generic;

namespace ATM_Origin.Core.Entities
{
    public partial class Operaciones
    {
        public int Id { get; set; }
        public int TarjetaId { get; set; }
        public DateTime Fecha { get; set; }
        public string CodigoOperacion { get; set; }
        public decimal? Balance { get; set; }

        public virtual Tarjeta Tarjeta { get; set; }
    }
}
