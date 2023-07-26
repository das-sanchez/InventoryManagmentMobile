using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class ReceptionHead
    {
        [JsonProperty("orderNo")]
        public string OrderNo { get; set; }
        [JsonProperty("billNo")]
        public string BillNo { get; set; }
        [JsonProperty("orderType")]
        public string OrderType { get; set; }


        [JsonProperty("items")]
        public ReceptionItem[] Items { get; set; }
    }
}
