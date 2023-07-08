using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class ReceptionItem
    {
        [JsonProperty("lineNo")]
        public int LineNo { get; set; }

        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("productBarCode")]
        public string ProductBarCode { get; set; }

        [JsonProperty("qty")]
        public decimal Qty { get; set; }

        [JsonProperty("factor")]
        public int Factor { get; set; }

        [JsonProperty("um")]
        public string Um { get; set; }

        [JsonProperty("bono")]
        public bool Bono { get; set; }

        [JsonProperty("lotNo")]
        public string LotNo { get; set; }

        [JsonProperty("lotExpirationDate")]
        public DateTime LotExpirationDate { get; set; }

    }
}
