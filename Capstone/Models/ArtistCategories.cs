using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Models
{
    public class ArtistCategories
    {
        [Key]
        public int ArtistCategoryId { get; set; }
        public string Name { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        [ForeignKey("Categories")]
        public int CategoriesId { get; set; }
        public Categories Category { get; set; }
    }

}

