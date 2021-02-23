using FilmsCatalog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FilmsCatalog.Domain.Entities
{
    public class FilmEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        public string ImgName { get; set; }

        [Required]
        [Column("CreatedAt")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDateTime { get; set; }

        [Column("CreatedBy")]
        public string CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public UserEntity CreatedByUser { get; set; }


    }
}
