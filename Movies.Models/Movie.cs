using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Models.Common;

namespace Movies.Models
{
    public class Movie : AuditableEntity
    {
        public string OriginalTitle { get; set; }
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
    }
}
