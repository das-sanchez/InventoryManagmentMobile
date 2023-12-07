using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class OrderItem
    {
        [JsonPropertyName("lineNo")]
        public int LineNo { get; set; }

        [JsonPropertyName("storeId")]

        public string StoreId { get; set; }

        [JsonPropertyName("storageId")]

        public string StorageId { get; set; }

        [JsonPropertyName("productId")]
        public string ProductId { get; set; }

        [JsonPropertyName("productBarCode")]
        public string ProductBarCode { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; }

        [JsonPropertyName("qty")]
        public decimal Qty { get; set; }

        [JsonPropertyName("um")]
        public string Um { get; set; }

        [JsonPropertyName("umName")]
        public string UmName { get; set; }

        [JsonPropertyName("productGroup")]
        public int ProductGroup { get; set; }
        [JsonProperty("isLotNoRequired")]
        public bool IsLotNoRequired { get; set; }
        [JsonProperty("isBonus")]
        public bool Bono { get; set; } = false;
    }
}
