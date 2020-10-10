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

        public async Task InsertOperacion(Operaciones operaciones)
        {
            _context.Operaciones.Add(operaciones);

            await _context.SaveChangesAsync();
        }
    }
}
