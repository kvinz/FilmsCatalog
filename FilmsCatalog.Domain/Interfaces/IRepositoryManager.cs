using FilmsCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmsCatalog.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IRepositoryBase<FilmEntity> Films { get; }

        Task<int> SaveChanges();
    }
}
