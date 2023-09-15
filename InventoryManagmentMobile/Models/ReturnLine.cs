using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace InventoryManagmentMobile.Models
{
    public class ReturnLine
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed(Name = "CompositeKey", Order = 1, Unique = true)]
        public string VendorNo { get; set; }

        [Indexed(Name = "CompositeKey", Order = 2, Unique = true)]
        public string ProductId { get; set; }
        public string ProductBarCode { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Um { get; set; }
        public ReturnLine() { }
    }
}
