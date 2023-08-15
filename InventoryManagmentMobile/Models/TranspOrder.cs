using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class TranspOrder
    {
        [JsonProperty("orderNo")]
        public string OrderNo { get; set; }

        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("vendorName")]
        public string VendorName { get; set; }

        [JsonProperty("items")]
        public TranspOrderItem[] Items { get; set; }
    }
}
