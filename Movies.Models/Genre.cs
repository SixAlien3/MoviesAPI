using System.Collections.Generic;
using Movies.Models.Common;
using Newtonsoft.Json;

namespace Movies.Models
{
    public class Genre: AuditableEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Movie> Movies { get; set; }
    }
}
