using ATM_Origin.Core.Entities;
using ATM_Origin.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Origin.Core.Services
{
   
    public class TarjetaService : ITarjetaService
    {
        private readonly ITarjetaRepository _tarjetaRepository;
        public TarjetaService(ITarjetaRepository tarjetaRepository)
        {
            _tarjetaRepository = tarjetaRepository;
        }
        public async Task<Tarjeta> GetTarjetaByNumber(string number)
        {
            return await _tarjetaRepository.GetTarjetaByNumber(number);
        }


        public async Task<Tarjeta> GetTarjetaByPin(Tarjeta tarjeta)
        {
            return await _tarjetaRepository.GetTarjetaByPin(tarjeta);
        }

        public async Task InsertOperacion(Operaciones operaciones)
        {
            await _tarjetaRepository.InsertOperacion(operaciones);
        }

        public async Task<bool> UpdateTarjeta(Tarjeta tarjeta)
        {
            return await _tarjetaRepository.UpdateTarjeta(tarjeta);
        }

        public async Task<Tarjeta> GetTarjeta(int id)
        {
            return await _tarjetaRepository.GetTarjeta(id);
        }
        public async Task<IEnumerable<Tarjeta>> GetTarjetas()
        {
            return await _tarjetaRepository.GetTarjetas();
        }
        public async Task<bool> DeleteTarjeta(int id)
        {
            return await _tarjetaRepository.DeleteTarjeta(id);
        }

    }
}
