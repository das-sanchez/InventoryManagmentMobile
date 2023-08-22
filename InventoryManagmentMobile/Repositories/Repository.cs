using InventoryManagmentMobile.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Repositories
{
    public class Repository<T> where T : class
    {
        private readonly OleDataContext _context;

        public Repository(OleDataContext context)
        {
            _context = context;
        }
    }
}
