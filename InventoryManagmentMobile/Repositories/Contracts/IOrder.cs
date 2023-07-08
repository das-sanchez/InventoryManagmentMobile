using InventoryManagmentMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Repositories.Contracts
{
    public interface IOrder
    {
        Task<OrderResult> OrderById(string OrderNo);
        void SaveOrder(Order order);
        void DeleteOrder(string OrderNo);
        Task<List<OrderResult>> GetAllOrders();
        Task<OrderItem> GetOrderItem(string OrderNo, int LineNo);

        void SaveOrderItem(OrderItem orderItem);
        void DeleteOrderItem(OrderItem orderItem);
        Task<List<OrderItem>> orderItems();


    }
}
