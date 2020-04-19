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
                new IdentityRole { Name = "Artist", NormalizedName = "ARTIST", Id = "07e98e2e-8bbf-457d-8564-a619c36ab27f", ConcurrencyStamp = "ca934950-ebfb-45a3-9ae5-9e483ab35528" },
                new IdentityRole { Name = "Consumer", NormalizedName = "CONSUMER", Id = "65bddf41-6651-40b8-80be-0bcbab5d89a9", ConcurrencyStamp = "24788613-e613-4e77-9f96-948472358639" }
                 );
            builder.Entity<Categories>()
               .HasData(
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
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Consumer> Consumer { get; set; }
        public DbSet<ConsumerRequest> ConsumerRequest { get; set; }
        public DbSet<ArtistCategories> ArtistCategories { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
    }
}
