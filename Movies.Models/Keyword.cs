using System.Collections.Generic;
using Movies.Models.Common;

namespace Movies.Models
{
    public class Keyword: AuditableEntity
    {
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
