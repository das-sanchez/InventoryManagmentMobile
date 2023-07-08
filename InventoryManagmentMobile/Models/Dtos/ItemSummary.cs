using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models.Dtos
{
    public class ItemSummary
    {
        public ItemSummary() { }

        [JsonPropertyName("lineNo")]
        public long LineNo { get; set; }

        [JsonPropertyName("productNo")]
        public string ProductNo { get; set; }

        [JsonPropertyName("productBarCode")]
        public string ProductBarCode { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("qty")]
        public double QtyOrder { get; set; }
        [JsonPropertyName("qty")]
        public double QtyReceive { get; set; }
        [JsonPropertyName("qty")]
        public double Qty { get; set; }

    }
}
