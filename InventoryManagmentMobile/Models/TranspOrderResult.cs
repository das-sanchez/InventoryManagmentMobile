using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace InventoryManagmentMobile.Models
{
    public class TranspOrderResult
    {
        [JsonProperty("data")]
        public TranspOrder Data { get; set; }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonPropertyName("messagesFromErp")]
        public MessagesFromErp[] messagesFromErp { get; set; }
    }
}
