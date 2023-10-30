using InventoryManagmentMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Repositories.Contracts
{
    public interface IOleRepository
    {
        Task<List<StoreResult>> Stores();
        Task<List<VendorResult>> Vendors();
        Task<List<VendorResult>> VendorsByName(string filter);
        Task<OrderResult> OrderByOrderNo(string OrderNo);
        Task<ProductResult> ProductById(string ProductNo);
        Task<ProductResult> ProductByBarCode(string BarCode);
        Task<VendorResult> VendorById(string VendorNo);
        Task<StoreResult> StoreByNo(string StoreNo);

        Task<TransResult> SaveDispatch(string OrderNo, OrderAdd order);
        Task<TransResult> SaveReception(string OrderNo, ReceptionHead order);
        Task<TransResult> SaveTransportationOrder(string OrderNo, OrderAdd order);
        Task<TransResult> SaveReturn(ReturnHead order);
        Task<StorageResult> StorageByNo(string StoreNo);
        Task<TransResult> SaveInboundDelivery(string OrderNo, ReceptionHead order);
        Task<OrderResult> InboundDeliveryByOrderNo(string OrderNo);
    }
}
