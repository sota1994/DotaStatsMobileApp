using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaApp.Models
{
    public class Hero
    {
        [JsonProperty(PropertyName ="id")]
        public int Id { get; set; }
        
        public string localized_name { get; set; }
    }
}
