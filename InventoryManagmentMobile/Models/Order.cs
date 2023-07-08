using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class Order
    {
        [JsonPropertyName("orderNo")]
        public string OrderNo { get; set; }

        [JsonPropertyName("companyId")]
        public long CompanyId { get; set; }

        [JsonPropertyName("vendorId")]
        public string VendorId { get; set; }

        [JsonPropertyName("vendorName")]
        public string VendorName { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdDate")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonPropertyName("paymentTermId")]
        public string PaymentTermId { get; set; }

        [JsonPropertyName("purchaseOrderOrg")]

        public long PurchaseOrderOrg { get; set; }

        [JsonPropertyName("purchaseOrderGroup")]
        public string PurchaseOrderGroup { get; set; }

        [JsonPropertyName("items")]
        public OrderItem[] Items { get; set; }
    }
}
