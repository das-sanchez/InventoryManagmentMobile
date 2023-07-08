using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class DetailDto
    {
        public string ProductId { get; set; }
        public string ProductBarCode { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Um { get; set; }
        public decimal Quantity { get; set; }
        public decimal QtyRecibida { get; set; }
        public decimal QtyPending { get; set; }
        public int Stock { get; set; }

    }
}
