using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmsCatalog.Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetAllQueryable();
        Task<T> GetAsync(int id);
        Task CreateAsync(T item);
        void Update(T item);
        Task DeleteAsync(int id);
    }
}
