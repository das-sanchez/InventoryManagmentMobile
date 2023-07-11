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
        private readonly ApiService<VendorResult> apiVendor;
        private readonly ApiService<TransResult> apiPost;
        public OleRepository() { apiOrder = new ApiService<OrderResult>(); apiProduct = new ApiService<ProductResult>(); apiStore = new ApiService<StoreResult>(); apiVendor = new ApiService<VendorResult>(); apiPost = new ApiService<TransResult>(); }

        public async Task<OrderResult> OrderByOrderNo(string OrderNo)
        {
            var order = new OrderResult();
            order = await apiOrder.GetData(Constants.UrlBase, $"order/{OrderNo}");
            return order;
        }

        public async Task<ProductResult> ProductByBarCode(string BarCode)
        {
            var product = new ProductResult();
            product = await apiProduct.GetData(Constants.UrlBase, $"product/barcode/{BarCode}");
            return product;
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
            return (TransResult)result;
        }

        public async Task<TransResult> SaveReception(string OrderNo, ReceptionHead order)
        {
            var result = await apiPost.PostData(Constants.UrlBase, $"order/{OrderNo}/Reception", order);

            return result;
        }
        public async Task<TransResult> SaveReturn(ReturnHead order)
        {
            var data = JsonConvert.SerializeObject(order);
            var result = await apiPost.PostData(Constants.UrlBase, $"order/return", order);

            return (TransResult)result;
        }

        public async Task<TransResult> SaveTransportationOrder(string OrderNo, OrderAdd order)
        {
            var result = await apiPost.PostData(Constants.UrlBase, $"transportationOrder/{OrderNo}/reception", order);

            return (TransResult)result;
        }

        public async Task<StoreResult> StoreByNo(string StoreNo)
        {
            var product = new StoreResult();
            product = await apiStore.GetData(Constants.UrlBase, $"store");
            return product;
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



    }
}
