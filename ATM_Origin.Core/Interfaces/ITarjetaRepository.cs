using ATM_Origin.Core.Entities;
using ATM_Origin.Core.RequestFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Origin.Core.Interfaces
{
    public interface ITarjetaRepository
    {
        Task<Tarjeta> GetTarjetaByNumber(string number);
        Task<Tarjeta> GetTarjetaByPin(TarjetaRequestFilter tarjeta);
        Task InsertOperacion(Operaciones operaciones);
        Task<bool> UpdateTarjeta(Tarjeta tarjeta);

        Task<IEnumerable<Tarjeta>> GetTarjetas();
        Task<Tarjeta> GetTarjeta(int id);
        Task<bool> DeleteTarjeta(int id);
    }
}
