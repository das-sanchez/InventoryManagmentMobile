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
    public class UserStore
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed(Name = "CompositeKey", Order = 1, Unique = true)]
        public string UserId { get; set; }
        public string StoreNo { get; set; }
    }
}
