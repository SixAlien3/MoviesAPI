using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
    }
}
