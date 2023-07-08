using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class OrderItemAdd
    {
        public string OrderNo { get; set; } // => DOCUMENT - CHAR(16)
        public int LineNo { get; set; } // => LINE - NUMC(4)
        public string StoreId { get; set; } // => PLANT - CHAR(4)
        public string StorageId { get; set; } // => STORAGE - CHAR(4)
        public string ProductNo { get; set; } // => MATERIAL - CHAR(40)
        public string ProductBarCode { get; set; } // => BAR_CODE - CHAR(18)
        public decimal Qty { get; set; } // => QUANTITY - QUAN(13,3)
        public int Factor { get; set; } // => FACTOR - dec(5)
        public string Um { get; set; } // => UM - UNIT(3)
        public bool Bono { get; set; } // => BONO - CHAR(1)
        public string LotNo { get; set; } // => LOT - CHAR(10)
        public DateTime LotExpirationDate { get; set; } // => 
    }
}
