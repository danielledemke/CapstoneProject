using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ConsumerRequest
    {
        public int ConsumerRequestId { get; set; }
        public DateTime DateTime { get; set; }
        public string Request { get; set; }

        [ForeignKey("Consumer")]
        public int ConsumerId { get; set; }
        public Consumer Consumer { get; set; }
    }
}
