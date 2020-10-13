using ATM_Origin.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Origin.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Tarjeta> TarjetaRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
