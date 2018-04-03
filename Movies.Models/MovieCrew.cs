using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Movies.Models
{
    public class MovieCrew 
    {
        [Key, Column(Order = 0)]
        public int MovieId { get; set; }

        [Key, Column(Order = 1)]
        [JsonProperty("id")]
        public int CrewId { get; set; }

        public Movie Movie { get; set; }
        public Crew Crew { get; set; }
    }
}