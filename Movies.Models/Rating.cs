using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Models.Common;

namespace Movies.Models
{
   public class Rating
    {
        [Key, Column(Order = 0)]
        public int MovieId { get; set; }

        [Key, Column(Order = 1)]
        public string CustomerId { get; set; }

        [Range(0,10)]
        public double UserRating { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }

        public Movie Movie { get; set; }

    }
}
