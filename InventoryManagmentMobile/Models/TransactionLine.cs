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
    public class TransactionLine
    {
        [Indexed(Name = "CompositeKey", Order = 1, Unique = true)]
        public string TypeTrans { get; set; }
        [Indexed(Name = "CompositeKey", Order = 2, Unique = true)]
        public string OrderNo { get; set; }
        [Indexed(Name = "CompositeKey", Order = 3, Unique = true)]
        public int LineNo { get; set; }
        public string ProductId { get; set; }
        public string ProductBarCode { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Um { get; set; }
        public decimal Quantity { get; set; }
        public decimal QtyRecibida { get; set; }
        public decimal QtyPending { get; set; }
        public bool Bono { get; set; }

        public TransactionLine() { }
    }
}
