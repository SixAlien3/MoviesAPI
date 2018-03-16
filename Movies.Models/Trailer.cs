using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Models.Common;
using Newtonsoft.Json;

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