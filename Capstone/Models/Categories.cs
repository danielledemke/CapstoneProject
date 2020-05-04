using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Models
{
    public class Categories
    {
        [Key]
        public int CategoriesId { get; set; }
        public string Name { get; set; }
    }
}
