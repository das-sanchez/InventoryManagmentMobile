using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class MessagesFromErp
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public object Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("nomber")]
        public object Nomber { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("logNo")]
        public string LogNo { get; set; }

        [JsonProperty("logMsgNo")]
        public string LogMsgNo { get; set; }

        [JsonProperty("messageV1")]
        public string MessageV1 { get; set; }

        [JsonProperty("messageV2")]
        public string MessageV2 { get; set; }

        [JsonProperty("messageV3")]
        public string MessageV3 { get; set; }

        [JsonProperty("messageV4")]
        public string MessageV4 { get; set; }

        [JsonProperty("row")]
        public object Row { get; set; }

        [JsonProperty("field")]
        public object Field { get; set; }

        [JsonProperty("system")]
        public object System { get; set; }

        [JsonProperty("systemRow")]
        public object SystemRow { get; set; }
    }
}
