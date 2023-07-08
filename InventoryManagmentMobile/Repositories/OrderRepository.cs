using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Repositories.Contracts;
using InventoryManagmentMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Repositories
{
    public class OrderRepository : IOrder
    {
        private readonly ApiService<OrderResult> apiOrder;
        public OrderRepository() { apiOrder = new ApiService<OrderResult>(); }

        public void DeleteOrder(string OrderNo)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderItem(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderResult>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> GetOrderItem(string OrderNo, int LineNo)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderResult> OrderById(string OrderNo)
        {
            var order = new OrderResult();
            order = await apiOrder.GetData(Constants.UrlBase, $"order/{OrderNo}");
            return order;
        }

        public Task<List<OrderItem>> orderItems()
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void SaveOrderItem(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }
    }
}
