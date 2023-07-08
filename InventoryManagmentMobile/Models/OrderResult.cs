using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class OrderResult
    {
        [JsonPropertyName("data")]
        public Order Data { get; set; }

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("erpErrorMessage")]
        public MessagesFromErp[] messagesFromErp { get; set; }
    }
}
