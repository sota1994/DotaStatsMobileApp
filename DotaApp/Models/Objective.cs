using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaApp.Models
{
    public class Objective
    {
        [JsonProperty(PropertyName =("time"))]
        public int Time { get; set; }

        [JsonProperty(PropertyName =("type"))]
        public string Type { get; set; }

        [JsonProperty(PropertyName =("key"))]
        public string Key { get; set; }
    }
}
