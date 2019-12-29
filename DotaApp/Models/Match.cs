using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaApp.Models
{
    public class Match
    {
        [JsonProperty(PropertyName = "match_id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "players")]
        public List<Player> Players { get; set; }
    }
}
