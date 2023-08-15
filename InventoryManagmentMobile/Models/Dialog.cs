using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Models
{
    public class Dialog
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public bool IsError { get; set; }
        public Dialog() { }
    }
}
