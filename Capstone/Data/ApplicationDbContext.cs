using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder) 
        { 
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Name = "Artist",
                    NormalizedName = "ARTIST"
                },
                 new IdentityRole
                 {
                     Name = "Consumer",
                     NormalizedName = "CONSUMER"
                 },
                 new Categories { CategoriesId = 1, Name = "Painting" },
                 new Categories { CategoriesId = 2, Name = "Digital" },
                 new Categories { CategoriesId = 3, Name = "Drawing" },
                 new Categories { CategoriesId = 4, Name = "Sculpture" },
                 new Categories { CategoriesId = 5, Name = "Clothing" },
                 new Categories { CategoriesId = 6, Name = "Crafts" },
                 new Categories { CategoriesId = 7, Name = "Woodwork" },
                 new Categories { CategoriesId = 8, Name = "Jewelry" },
                 new Categories { CategoriesId = 9, Name = "Photography" }

             );
        }
    }
}
