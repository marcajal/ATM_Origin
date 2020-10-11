using System;
using System.Collections.Generic;

namespace ATM_Origin.Core.Entities
{
    public partial class Tarjeta
    {
        public Tarjeta()
        {
            Operaciones = new HashSet<Operaciones>();
        }

        public int Id { get; set; }
        public string Numero { get; set; }
        public DateTime FechaVto { get; set; }
        public string Pin { get; set; }
        public decimal Balance { get; set; }
        public bool Habilitada { get; set; }
        

        public virtual ICollection<Operaciones> Operaciones { get; set; }
    }
}
