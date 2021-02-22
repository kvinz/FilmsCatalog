using FilmsCatalog.Domain.EF;
using FilmsCatalog.Domain.Entities;
using FilmsCatalog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilmsCatalog.Domain.Repositories
{
    public class FilmRepository : IRepositoryBase<FilmEntity>
    {
        private readonly ApplicationDbContext _context;

        public  FilmRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<FilmEntity> GetAll()
        {
            return _context.Films;
        }

        public FilmEntity Get(int id)
        {
            return _context.Films.Find(id);
        }

        public void Create(FilmEntity entity)
        {
            _context.Films.Add(entity);
        }

        public void Update(FilmEntity entity)
        {
            _context.Films.Update(entity);
        }

        public IEnumerable<FilmEntity> Find(Func<FilmEntity, Boolean> predicate)
        {
            return _context.Films.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            FilmEntity book = _context.Films.Find(id);
            if (book != null)
                _context.Films.Remove(book);
        }
    }
}
