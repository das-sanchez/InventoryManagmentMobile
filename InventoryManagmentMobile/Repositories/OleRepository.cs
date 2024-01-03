using InventoryManagmentMobile.Models;
using InventoryManagmentMobile.Repositories.Contracts;
using InventoryManagmentMobile.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Repositories
{
    public class OleRepository : IOleRepository
    {
        private readonly ApiService<OrderResult> apiOrder;
        private readonly ApiService<ProductResult> apiProduct;
        private readonly ApiService<StoreResult> apiStore;
        private readonly ApiService<UserStoresResult> apiUserStore;
        private readonly ApiService<VendorResult> apiVendor;
        private readonly ApiService<TransResult> apiPost;
        private readonly ApiService<StorageResult> apiStorage;
        private readonly ApiService<TranspOrderResult> apiTraspe;
        private readonly ApiService<LoginResult> loginPost;

        public OleRepository() { 
            apiOrder = new ApiService<OrderResult>();                      
            apiProduct = new ApiService<ProductResult>(); 
            apiStore = new ApiService<StoreResult>();
            apiUserStore = new ApiService<UserStoresResult>();
            apiVendor = new ApiService<VendorResult>(); 
            apiPost = new ApiService<TransResult>(); 
            apiStorage = new ApiService<StorageResult>(); 
            apiTraspe = new ApiService<TranspOrderResult>(); 
            loginPost = new ApiService<LoginResult>(); 
        }

        public async Task<OrderResult> OrderByOrderNo(string OrderNo)
        {
            try
            {
                var order = new OrderResult();
                order = await apiOrder.GetData(Constants.UrlBase, $"order/{OrderNo}");
                return order;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ProductResult> ProductByBarCode(string BarCode)
        {
            return await apiProduct.GetData(Constants.UrlBase, $"product/barcode/{BarCode}");
        }

        public async Task<ProductResult> ProductById(string ProductNo)
        {
            var product = new ProductResult();
            product = await apiProduct.GetData(Constants.UrlBase, $"product/{ProductNo}");
            return product;
        }

        public async Task<TransResult> SaveDispatch(string OrderNo, OrderAdd order)
        {
            var result = await apiPost.PostData(Constants.UrlBase, $"order/{OrderNo}/Dispatch", order);
            return result;
        }

        public async Task<TransResult> SaveReception(string OrderNo, ReceptionHead order)
        {
            var result = new TransResult();

            if (order.OrderType != "T")
            {
                result = await apiPost.PostData(Constants.UrlBase, $"order/{OrderNo}/Reception", order);
            }
            else
            {
                result = await apiPost.PostData(Constants.UrlBase, $"inboundDelivery", order);
            }

            return result;
        }
        public async Task<TransResult> PrintDiff(string OrderNo, ReceptionHead order)
        {
            var result = new TransResult();

            if (order.OrderType != "T")
            {
                result = await apiPost.PostData(Constants.UrlBase, $"order/{OrderNo}/Dispatch/PrintDiff", order);
            }
            else
            {
                result = await apiPost.PostData(Constants.UrlBase, $"inboundDelivery/PrintDiff", order);
            }

            return result;
        }

        public async Task<TransResult> SaveReturn(ReturnHead order)
        {
            var result = await apiPost.PostData(Constants.UrlBase, $"order/return", order);
            return (TransResult)result;
        }

        public async Task<TransResult> SaveTransportationOrder(string OrderNo, OrderAdd order)
        {
            var result = await apiPost.PostData(Constants.UrlBase, $"transportationOrder/{OrderNo}/reception", order);

            return (TransResult)result;
        }

        public async Task<StorageResult> StorageByNo(string StoreNo)
        {
            var product = new StorageResult();
            product = await apiStorage.GetData(Constants.UrlBase, $"store/{StoreNo}/storages");
            return product;
        }

        public async Task<StoreResult> StoreByNo(string StoreNo)
        {
            var product = new StoreResult();
            product = await apiStore.GetData(Constants.UrlBase, $"store");
            return product;
        }

        public async Task<UserStoresResult> GetStoreListByUser(string username)
        {
            var stores = new UserStoresResult();
            stores = await apiUserStore.GetData(Constants.UrlBase, $"User/GetStoresByUser/{username}");
            return stores;
        }

        public async Task<List<StoreResult>> Stores()
        {

            var storeList = new List<StoreResult>();
            storeList = await apiStore.GetListData(Constants.UrlBase, $"store");
            return storeList;
        }

        public async Task<VendorResult> VendorById(string VendorNo)
        {
            var product = new VendorResult();
            product = await apiVendor.GetData(Constants.UrlBase, $"vendor/{VendorNo}");
            return product;
        }

        public async Task<List<VendorResult>> Vendors()
        {
            var storeList = new List<VendorResult>();
            storeList = await apiVendor.GetListData(Constants.UrlBase, $"vendor");
            return storeList;
        }

        public async Task<List<VendorResult>> VendorsByName(string filter)
        {
            var storeList = new List<VendorResult>();
            storeList = await apiVendor.GetListData(Constants.UrlBase, $"vendor/FilterText/{filter}");
            return storeList;
        }
        public async Task<TranspOrderResult> TranspOrderByOrderNo(string OrderNo)
        {
            var order = new TranspOrderResult();
            order = await apiTraspe.GetData(Constants.UrlBase, $"TransportationOrder/{OrderNo}/SummarizedByProduct");
            return order;
        }
        public async Task<TransResult> SaveTransporationOrder(string OrderNo, TransportationOrder order)
        {
            var result = await apiPost.PostData(Constants.UrlBase, $"transportationOrder/{OrderNo}/Reception", order);
            return result;
        }
        public async Task<LoginResult> Login(Login login)
        {
            var result = await loginPost.PostData(Constants.UrlBase, $"user/login", login);

            return result;
        }

        public async Task<TransResult> SaveInboundDelivery(string OrderNo, ReceptionHead order)
        {
            var result = await apiPost.PostData(Constants.UrlBase, $"inboundDelivery", order);

            return result;
        }

        public async Task<OrderResult> InboundDeliveryByOrderNo(string OrderNo)
        {
            try
            {
                var order = new OrderResult();
                order = await apiOrder.GetData(Constants.UrlBase, $"InboundDelivery/{OrderNo}");
                return order;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
