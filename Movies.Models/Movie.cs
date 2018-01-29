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
    public class Movie : AuditableEntity
    {
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
        public string TmdbId { get; set; }
        public string PosterUrl { get; set; }
        public string OriginalLanguage { get; set; }
        public decimal Popularity { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int RunTime { get; set; }
        public bool? IsReleased { get; set; }
        public decimal? AverageVote { get; set; }
        public int? VoteCount { get; set; }
        public int ExternalId { get; set; }

        public ICollection<Genre> Genres { get; set; }
        public ICollection<MovieCasts> MovieCastses { get; set; }
        public ICollection<Keyword> Keywords { get; set; }
    }
}
