using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("barCode")]
        public string BarCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("baseUm")]
        public string BaseUm { get; set; }

        [JsonProperty("factor")]
        public int Factor { get; set; }

        [JsonProperty("weightUm")]
        public string WeightUm { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("isLotNoRequired")]
        public bool IsLotNoRequired { get; set; }


        [JsonProperty("measurementUnits")]
        public MeasurementUnit[] MeasurementUnits { get; set; }
    }
}
