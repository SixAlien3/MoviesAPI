using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Models.Common;
using Newtonsoft.Json;

namespace Movies.Models
{
    public class Movie : AuditableEntity
    {
        [JsonIgnore]
        public string OriginalTitle { get; set; }

        [Index(IsUnique = false)]
        [StringLength(128)]
        public string Title { get; set; }

        public string Overview { get; set; }
        public string Tagline { get; set; }
        public decimal Budget { get; set; }
        public decimal Revenue { get; set; }
        public string WebsiteUrl { get; set; }
        public string ImdbId { get; set; }

        [JsonIgnore]
        public string TmdbId { get; set; }

        public string PosterUrl { get; set; }
        public string BackdropUrl { get; set; }
        public string OriginalLanguage { get; set; }

        public decimal Popularity { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public int RunTime { get; set; }

        [JsonIgnore]
        public bool? IsReleased { get; set; }

        public decimal? AverageVote { get; set; }

        [JsonIgnore]
        public int? VoteCount { get; set; }

        [JsonIgnore]
        public int ExternalId { get; set; }

        public ICollection<Genre> Genres { get; set; }

        [JsonIgnore]
        public ICollection<MovieCasts> MovieCastses { get; set; }

        [JsonIgnore]

        public ICollection<MovieCrew> MovieCrews { get; set; }

        [JsonIgnore]
        public ICollection<Keyword> Keywords { get; set; }
    }
}