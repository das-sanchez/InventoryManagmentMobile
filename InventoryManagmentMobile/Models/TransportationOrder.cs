using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class TransportationOrder
    {
        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("items")]
        public TransportationOrderItem[] Items { get; set; }
    }
}
