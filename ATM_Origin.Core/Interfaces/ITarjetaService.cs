using ATM_Origin.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Origin.Core.Interfaces
{
    public interface ITarjetaService
    {
        Task<bool> DeleteTarjeta(int id);
        Task<Tarjeta> GetTarjeta(int id);
        Task<Tarjeta> GetTarjetaByNumber(string number);
        Task<Tarjeta> GetTarjetaByPin(Tarjeta tarjeta);
        Task<IEnumerable<Tarjeta>> GetTarjetas();
        Task InsertOperacion(Operaciones operaciones);
        Task<bool> UpdateTarjeta(Tarjeta tarjeta);
    }
}
