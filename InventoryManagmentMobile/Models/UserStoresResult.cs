﻿using InventoryManagmentMobile.Models.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class UserStoresResult
    {
        [JsonProperty("data")]
        public UserStoreDto[] Data { get; set; }

        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("messagesFromErp")]
        public MessagesFromErp[] MessagesFromErp { get; set; }
    }
}