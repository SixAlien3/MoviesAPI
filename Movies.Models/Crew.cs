using Movies.Models.Common;
using Newtonsoft.Json;

namespace Movies.Models
{
    public class Crew : AuditableEntity
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
        public string Job { get; set; }

        [JsonProperty("id")]
        public int ExternalId { get; set; }

        public string ProfilePath { get; set; }
    }
}