using FilmsCatalog.Domain.EF;
using FilmsCatalog.Domain.Entities;
using FilmsCatalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FilmsCatalog.Domain.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;
        FilmRepository filmRepository;

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }
               
        public IRepositoryBase<FilmEntity> Films
        {
            get
            {
                if (filmRepository == null)
                    filmRepository = new FilmRepository(_context);
                return filmRepository;
            }
        }
            

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
