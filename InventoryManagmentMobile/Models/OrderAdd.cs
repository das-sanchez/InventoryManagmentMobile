using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class OrderAdd
    {
        public string OrderNo { get; set; } // => DOCUMENT - CHAR(16)
        public DateTime OrderDate { get; set; } // => DOC_DATE - DATS(8) Formato YYYY-MM-DD
        public string Comment { get; set; } // => HEADER_TXT - CHAR(25)
        public string TransctionId { get; set; } // => DEL_NOTE - CHAR(10) -- Se utiliza para enviar el id generado por la Pocket.
        public string Activity { get; set; } // (Considerar cambiar por OrderType o DocumentType) => ACTIVITY - CHAR(2) En este campo se indica la clase de operación que se va a realizar dependiendo del proceso. Para los procesos 1,2, 4 y 5 se utilizaría A01.
        public string CreatedBy { get; set; } // => AUTOR - CHAR(12) - Es el usuario que creó el pedido en SAP, se valida contra el maestro de usuarios de SAP; pero ese campo sería solo de lectura para la pocket.
        public DateTime TransactionDate { get; set; } // => TRANS_DATE - DATS(8) Formato YYYY-MM-DD
        public List<OrderItemAdd> Items { get; set; }
    }
}
