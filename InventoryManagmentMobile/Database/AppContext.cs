using InventoryManagmentMobile.Models;
using Microsoft.EntityFrameworkCore;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Database
{
    public class AppContext
    {
        SQLiteAsyncConnection Database;
        public AppContext()
        {
            Init();
        }
        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await Database.CreateTableAsync<Order>();
            await Database.CreateTableAsync<OrderItem>();
            await Database.CreateTableAsync<OrderAdd>();
            await Database.CreateTableAsync<OrderItemAdd>();
        }
    }
}
