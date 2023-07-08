using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class MeasurementUnit
    {
        [JsonProperty("baseUm")]
        public string BaseUm { get; set; }

        [JsonProperty("factor")]
        public int Factor { get; set; }

        [JsonProperty("weightUm")]
        public string WeightUm { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }
    }
}
