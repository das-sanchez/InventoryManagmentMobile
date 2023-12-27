using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class TranspOrderItem
    {
        [JsonProperty("storeId")]
        public string StoreId { get; set; }

        [JsonProperty("storageId")]
        public string StorageId { get; set; }

        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("productBarCode")]
        public string ProductBarCode { get; set; }

        [JsonProperty("qty")]
        public decimal Qty { get; set; }

        [JsonProperty("um")]
        public string Um { get; set; }

        [JsonProperty("umName")]
        public string UmName { get; set; }
    }
}
