using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class ErrorMessage
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public int Nomber { get; set; }
        public string Message { get; set; }
        public string LogNo { get; set; }
        public int LogMsgNo { get; set; }
        public string MessageV1 { get; set; }
        public string MessageV2 { get; set; }
        public string MessageV3 { get; set; }
        public string MessageV4 { get; set; }
        public string Row { get; set; }
        public int Field { get; set; }
        public string System { get; set; }
        public string SystemRow { get; set; }
    }
}
