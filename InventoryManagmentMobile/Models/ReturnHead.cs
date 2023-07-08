using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class ReturnHead
    {

        [JsonProperty("comment")]
        public string Comment { get; set; }


        [JsonProperty("vendorId")]
        public string VendorId { get; set; }

        [JsonProperty("items")]
        public ReturnItem[] Items { get; set; }
    }
}
