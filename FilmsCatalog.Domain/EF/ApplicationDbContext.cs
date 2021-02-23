using FilmsCatalog.Domain.Entities;
using FilmsCatalog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsCatalog.Domain.EF
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FilmEntity> Films { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FilmEntity>(v =>
            {
                v.HasKey(k => k.Id);
                v.Property(p => p.Id)
                    .ValueGeneratedOnAdd();
                v.Property(p => p.Name)
                    .IsRequired();
                v.Property(p => p.Description)
                    .IsRequired();
                v.Property(p => p.ReleaseYear)
                    .IsRequired();
                v.Property(p => p.Director)
                    .IsRequired();
                v.Property(p => p.ImgName)
                    .IsRequired();
                v.Property(p => p.CreatedDateTime)
                    .IsRequired();
                v.HasOne(o => o.CreatedByUser)
                    .WithMany(m => m.Films)
                    .HasForeignKey(fk => fk.CreatedBy)
                    .HasPrincipalKey(pk => pk.Id);
            });
        }
    }
}
