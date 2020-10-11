using ATM_Origin.Core.Entities;
using ATM_Origin.Core.Interfaces;
using ATM_Origin_Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Origin_Infrastucture.Repositories
{
    public class TarjetaRepository : ITarjetaRepository
    {
        private readonly ATM_OriginDBContext _context;
        public TarjetaRepository(ATM_OriginDBContext context)
        {
            _context = context;
        }

        //1)********************************************************************************************************************
        public async Task<Tarjeta> GetTarjetaByNumber(string number)
        {
            return await _context.Tarjeta.FirstOrDefaultAsync(t => t.Numero == number && t.Habilitada == true);
        }
        //2)********************************************************************************************************************
        public async Task<Tarjeta> GetTarjetaByPin(Tarjeta tarjeta)
        {
            return await _context.Tarjeta.FirstOrDefaultAsync(t => t.Numero == tarjeta.Numero && t.Pin == tarjeta.Pin && t.Habilitada == true);
        }
        //3)********************************************************************************************************************
        public async Task InsertOperacion(Operaciones operaciones)
        {
            _context.Operaciones.Add(operaciones);
            await _context.SaveChangesAsync();
        }
        //4)********************************************************************************************************************
        public async Task<bool> UpdateTarjeta(Tarjeta tarjeta)
        {
            var currentTarjeta = await GetTarjeta(tarjeta.Id);
            currentTarjeta.Habilitada = tarjeta.Habilitada;
            currentTarjeta.Balance = tarjeta.Balance;
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }



        public async Task<IEnumerable<Tarjeta>> GetTarjetas()
        {
            var listTarjetas = await _context.Tarjeta.ToListAsync();
            
            return listTarjetas;
        }

        public async Task<Tarjeta> GetTarjeta(int id)
        {
            var tarjeta = await _context.Tarjeta.FirstOrDefaultAsync(t => t.Id == id);

            return tarjeta;
        }

        public async Task<bool> DeleteTarjeta(int id)
        {
            var currentTarjeta = await GetTarjeta(id);
            _context.Tarjeta.Remove(currentTarjeta);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
