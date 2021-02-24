using FilmsCatalog.Domain.EF;
using FilmsCatalog.Domain.Entities;
using FilmsCatalog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsCatalog.Domain.Repositories
{
    public class FilmRepository : IRepositoryBase<FilmEntity>
    {
        private readonly ApplicationDbContext _context;

        public  FilmRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FilmEntity>> GetAllAsync()
        {
            return await _context.Films.ToListAsync();
        }

        public  IQueryable<FilmEntity> GetAllQueryable()
        {
            IQueryable<FilmEntity> filmsQueryable = _context.Films;
            return filmsQueryable;
        }

        public async Task<FilmEntity> GetAsync(int id)
        {
            return await _context.Films
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(FilmEntity entity)
        {
            await _context.Films.AddAsync(entity);
        }

        public void Update(FilmEntity entity)
        {
            _context.Films.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            FilmEntity book =  await _context.Films.FindAsync(id);
            if (book != null)
                _context.Films.Remove(book);
        }
    }
}
