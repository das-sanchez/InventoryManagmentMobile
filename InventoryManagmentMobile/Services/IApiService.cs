using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Services
{
    public interface IApiService<T> 
    {
        Task<List<T>> GetListData(string Url);
        Task<T> PostData(string Url,  IEnumerable<T> Ltrans);
    }
}
