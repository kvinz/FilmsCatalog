﻿using FilmsCatalog.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FilmsCatalog.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public ICollection<FilmEntity> Films { get; set; }
    }
}