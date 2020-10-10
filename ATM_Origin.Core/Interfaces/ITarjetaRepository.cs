using ATM_Origin.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Origin.Core.Interfaces
{
    public interface ITarjetaRepository
    {
        Task<IEnumerable<Tarjeta>> GetTarjetas();
        Task<Tarjeta> GetTarjeta(int id);
        Task InsertOperacion(Operaciones operaciones);
    }
}
