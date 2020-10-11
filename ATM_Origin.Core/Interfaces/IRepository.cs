using ATM_Origin.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Origin.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
