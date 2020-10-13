using ATM_Origin.Core.Entities;
using ATM_Origin.Core.Interfaces;
using ATM_Origin_Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Origin_Infrastucture.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ATM_OriginDBContext _context;
        private readonly IRepository<Tarjeta> _tarjetaRepository;

        public UnitOfWork(ATM_OriginDBContext context)
        {
            _context = context;
        }
        public IRepository<Tarjeta> TarjetaRepository => _tarjetaRepository ?? new BaseRepository<Tarjeta>(_context);

        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
          await _context.SaveChangesAsync();
        }
    }
}
