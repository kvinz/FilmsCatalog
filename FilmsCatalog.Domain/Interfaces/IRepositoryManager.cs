using FilmsCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsCatalog.Domain.Interfaces
{
    public interface IRepositoryManager
    {
        IRepositoryBase<FilmEntity> Films { get; }

        int SaveChanges();
    }
}
