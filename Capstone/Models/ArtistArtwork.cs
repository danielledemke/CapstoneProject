using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ArtistArtwork
    {
        public int ArtistArtworkId { get; set; }
        public string Name { get; set; }
        [Display(Name = "In Stock")]
        public bool InStock { get; set; }
        [Display(Name = "Artwork Image URL")]
        public string ImgUrl { get; set; }
        [Display(Name = "Artwork Price")]
        public double ArtworkPrice { get; set; }

        [ForeignKey("Artist")]
        public int? ArtistId { get; set; }
        public Artist Artist { get; set; }




    }
}
