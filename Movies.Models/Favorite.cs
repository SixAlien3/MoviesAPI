using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Movies.Models
{
    public class Favorite
    {
        [Key, Column(Order = 0)]
        [JsonIgnore]
        public int MovieId { get; set; }

        [Key, Column(Order = 1)]
        [JsonIgnore]
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        [JsonIgnore]
        public virtual ApplicationUser Customer { get; set; }

        public Movie Movie { get; set; }
    }
}