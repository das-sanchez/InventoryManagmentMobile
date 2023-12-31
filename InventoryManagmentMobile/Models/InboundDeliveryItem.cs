﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class InboundDeliveryItem
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

        [JsonPropertyName("productGroup")]
        public int ProductGroup { get; set; }
        public InboundDeliveryItem() { }
    }
}
