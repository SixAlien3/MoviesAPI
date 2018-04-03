using Movies.Models.Common;

namespace Movies.Models
{
    public class Trailer : AuditableEntity
    {
        public string Url { get; set; }
        public string Type { get; set; }
        public string Site { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}