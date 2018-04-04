using Movies.Models.Common;
using Newtonsoft.Json;

namespace Movies.Models
{
    public class Cast: AuditableEntity
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Order { get; set; }
        [JsonProperty("id")]
        public int ExternalId { get; set; }
        public string ProfilePath { get; set; }
    }
}
