using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models.Dtos
{
    public class UserStoreDto
    {
        [JsonProperty("storeId")]
        public string StoreId { get; set; }
        [JsonProperty("storeName")]
        public string StoreName { get; set; }

    }
}
