using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaApp.Models
{
    public class Player
    {
        [JsonProperty(PropertyName ="hero_id")]
       public int HeroId { get; set; }

        [JsonProperty(PropertyName = "account_id")]
       public long? SteamId { get; set; }

        [JsonProperty(PropertyName = "gold_per_min")]
       public int GPM { get; set; }

        [JsonProperty(PropertyName = "xp_per_min")]
       public int XPM { get; set; }

        [JsonProperty(PropertyName = "total_gold")]
       public long Networth { get; set; }

        [JsonProperty(PropertyName ="total_xp")]
       public long TotalXP { get; set; }

        [JsonProperty(PropertyName = "isRadiant")]
       public bool IsRadiant { get; set; }
        
        [JsonProperty(PropertyName = "neutral_kills")]
        public int NeutralFarm { get; set; }
        
        [JsonProperty(PropertyName ="lane_kills")]
        public int LaneFarm { get; set; }
    }
}
