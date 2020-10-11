using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Origin.Core.RequestFilters
{
    public class TarjetaRequestFilter
    {
        public string Numero { get; set; }
        public string Pin { get; set; }
        public bool Habilitada { get; set; }
    }
}
