using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class InboundDelivery
    {
        [JsonProperty("orderNo")]
        public string OrderNo { get; set; }

        [JsonProperty("items")]
        public InboundDeliveryItem[] Items { get; set; }
        public InboundDelivery() { }

    }
}
