using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Models
{
    public class ArtworkOrder
    {
        public int ArtworkOrderId { get; set; }
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }
        [Display(Name = "Shipped Date")]
        public DateTime ShippedDate { get; set; }

        [ForeignKey("ArtistArtwork")]
        public int? ArtistArtworkId { get; set; }
        public ArtistArtwork ArtistArtwork { get; set; }

        [ForeignKey("Artist")]
        public int? ArtistId { get; set; }
        public Artist Artist { get; set; }


        [ForeignKey("Consumer")]
        public int? ConsumerId { get; set; }
        public Consumer Consumer { get; set; }


    }
}
