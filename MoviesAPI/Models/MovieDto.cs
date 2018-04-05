using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Movies.Models;
using Newtonsoft.Json;

namespace MoviesAPI.Models
{
    public class MovieDto
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string OriginalTitle { get; set; }

        [Index(IsUnique = false)]
        [StringLength(128)]
        public string Title { get; set; }

        public string Overview { get; set; }
        public string Tagline { get; set; }
        public decimal Budget { get; set; }
        public decimal Revenue { get; set; }

        [JsonProperty("homepage")]
        public string HomePage { get; set; }

        public string ImdbId { get; set; }

        [JsonIgnore]
        public string TmdbId { get; set; }

        public string PosterUrl { get; set; }
        public string BackdropUrl { get; set; }
        public string OriginalLanguage { get; set; }

        public decimal Popularity { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public int RunTime { get; set; }
        public bool? IsReleased => !ReleaseDate.HasValue || ReleaseDate.Value.Date >= DateTime.Now;
        public string Status { get; set; }
        public decimal? AverageVote { get; set; }

        [JsonIgnore]
        public int? VoteCount { get; set; }

        public int ExternalId { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Keyword> Keywords { get; set; }
        public ICollection<Trailer> Trailers { get; set; }
    }
}