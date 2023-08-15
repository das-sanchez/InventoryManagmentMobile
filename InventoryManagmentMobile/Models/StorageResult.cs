using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.Controls.Internals.Profile;

namespace InventoryManagmentMobile.Models
{
    public partial class StorageResult
    {
        [JsonProperty("data")]
        public Storage[] Data { get; set; }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("messagesFromErp")]
        public MessagesFromErp[] MessagesFromErp { get; set; }
    }
}
