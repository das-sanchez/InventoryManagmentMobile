using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public object Message { get; set; }

        [JsonProperty("messagesFromErp")]
        public object MessagesFromErp { get; set; }
    }
}
