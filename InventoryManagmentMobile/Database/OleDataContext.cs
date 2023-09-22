
using InventoryManagmentMobile.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Maui.Controls.Shapes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentMobile.Database
{
    public class OleDataContext : IDisposable
    {
        private readonly SQLiteConnection database;
        public OleDataContext()
        {
            var dbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "oleDb.db");
            database = new SQLiteConnection(dbPath);
            database.CreateTable<TransactionLine>();
            database.CreateTable<ReturnLine>();
            database.CreateTable<UserStore>();
        }
        public List<TransactionLine> GetTransactionLines()

        {
            return database.Table<TransactionLine>().ToList();
        }

        public int CreateTransactionLine(TransactionLine line)
        {
            return database.Insert(line);
        }

        public int UpdateTransactionLine(TransactionLine line)
        {
            return database.Update(line);
        }
        public int SaveTransactionLine(List<TransactionLine> lines)
        {
            try
            {
                lines.ForEach((l) =>
                {
                    if (database.Table<TransactionLine>().Any(xc => xc.LineNo == l.LineNo && xc.OrderNo == l.OrderNo && xc.ProductBarCode == l.ProductBarCode && xc.Bono == l.Bono))
                    {
                        database.Update(l);
                    }
                    else
                    {
                        database.Insert(l);
                    }
                });

                return lines.Count();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public int SaveTransLine(TransactionLine l)
        {
            try
            {
                if (database.Table<TransactionLine>().Any(xc => xc.LineNo == l.LineNo && xc.OrderNo == l.OrderNo && xc.ProductBarCode == l.ProductBarCode && xc.Bono == l.Bono))
                {
                    return database.Update(l);
                }
                else
                {
                    return database.Insert(l);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int DeleteTransationLine(TransactionLine line)
        {
            return database.Delete(line);
        }

        public int DeleteTransationLineByOrderNo(string OrderNo)
        {
            var list = database.Table<TransactionLine>().Where(a => a.OrderNo == OrderNo).ToList();
            if (list.Count() == 0)
            {
                return 0;
            }
            list.ForEach((line) => { database.Delete(line); });
            return list.Count();
        }
        public int DeleteTransationLineByOrderNo(string Type, string OrderNo)
        {

            return database.Execute($"DELETE FROM TransactionLine Where TypeTrans = '{Type}' AND OrderNo ='{OrderNo}'");
        }
        public int ExecuteSql(string sql)
        {
            return database.Execute(sql);
        }

        public int ValidExist(string filter)
        {
            return database.Query<int>($"SELECT Count(ProductBarCode) Existe FROM TransactionLine WHERE {filter}").FirstOrDefault();
        }
        public bool ValidExist(string type, string orderno, string productNo, bool IsBonus = false)
        {
            return database.Table<TransactionLine>().Any(a => a.ProductBarCode == productNo && a.TypeTrans == type && a.OrderNo == orderno && a.Bono == IsBonus);
        }
        public TransactionLine GetLine(string type, string orderno, string productNo, bool IsBonus = false)
        {
            return database.Table<TransactionLine>().Where(a => a.ProductBarCode == productNo && a.TypeTrans == type && a.OrderNo == orderno && a.Bono == IsBonus).FirstOrDefault();
        }
        public List<TransactionLine> GetTransactionLinesByOrderNo(string typetrans, string orderNo)
        {
            return database.Table<TransactionLine>().Where(a => a.TypeTrans == typetrans && a.OrderNo == orderNo).ToList();
        }
        public List<TransactionLine> GetTransactionLinesByOrderNo(string typetrans, string orderNo, string productNo)
        {
            if (productNo == "" || productNo == "*" | productNo == "ALL")
            {
                return database.Table<TransactionLine>().Where(a => a.TypeTrans == typetrans && a.OrderNo == orderNo).ToList();
            }
            else
            {
                return database.Table<TransactionLine>().Where(a => a.TypeTrans == typetrans && a.OrderNo == orderNo && a.ProductBarCode == productNo).ToList();
            }

        }
        public int NextTransactionLinesByOrderNo(string typetrans, string orderNo)
        {
            int nextline = 0;
            if (!database.Table<TransactionLine>().Any(a => a.TypeTrans == typetrans && a.OrderNo == orderNo))
            {
                nextline = 10;
            }
            else
            {
                nextline = (database.Table<TransactionLine>().Where(a => a.TypeTrans == typetrans && a.OrderNo == orderNo).Max(x => x.LineNo) + 10);
            }

            return nextline;
        }
        public TransactionLine GetTransactionLinesByProductNo(string ProductNo)
        {
            return database.Table<TransactionLine>().Where(a => a.ProductBarCode == ProductNo).FirstOrDefault();
        }
        //Return Lines
        public List<ReturnLine> GetReturnLines()

        {
            return database.Table<ReturnLine>().ToList();
        }

        public int CreateReturnLine(ReturnLine line)
        {
            return database.Insert(line);
        }

        public int UpdateReturnLine(ReturnLine line)
        {
            return database.Update(line);
        }

        public int DeleteReturnLine(ReturnLine line)
        {
            return database.Delete(line);
        }
        public int SaveReturnLine(ReturnLine l)
        {
            try
            {
                if (database.Table<ReturnLine>().Any(xc => xc.VendorNo == l.VendorNo && xc.ProductBarCode == l.ProductBarCode))
                {
                    return database.Update(l);
                }
                else
                {
                    return database.Insert(l);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int DeleteReturnLineByVendorNo(string VendorNo)
        {
            var list = database.Table<ReturnLine>().Where(a => a.VendorNo == VendorNo).ToList();
            if (list.Count() == 0)
            {
                return 0;
            }
            list.ForEach((line) => { database.Delete(line); });
            return list.Count();
        }

        public bool ValidExistReturnLine(string VendorNo, string productNo)
        {
            return database.Table<ReturnLine>().Any(a => a.ProductBarCode == productNo && a.VendorNo == VendorNo);
        }
        public ReturnLine GetReturnLine(string VendorNo, string productNo)
        {
            return database.Table<ReturnLine>().Where(a => a.ProductBarCode == productNo && a.VendorNo == VendorNo).FirstOrDefault();
        }
        public List<ReturnLine> GetReturnLinesByOrderNo(string VendorNo)
        {
            return database.Table<ReturnLine>().Where(a => a.VendorNo == VendorNo).ToList();
        }
        public List<ReturnLine> GetReturnLinesByOrderNo(string VendorNo, string productNo)
        {
            if (productNo == "" || productNo == "*" | productNo == "ALL")
            {
                return database.Table<ReturnLine>().Where(a => a.VendorNo == VendorNo).ToList();
            }
            else
            {
                return database.Table<ReturnLine>().Where(a => a.VendorNo == VendorNo && a.ProductBarCode == productNo).ToList();
            }

        }
        public ReturnLine GetReturnLinesByProductNo(string ProductNo)
        {
            return database.Table<ReturnLine>().Where(a => a.ProductBarCode == ProductNo).FirstOrDefault();
        }
        //User Store 
        public List<UserStore> GetUserStores()

        {
            return database.Table<UserStore>().ToList();
        }

        public int CreateUserStore(UserStore line)
        {
            return database.Insert(line);
        }

        public int UpdateUserStore(UserStore line)
        {
            return database.Update(line);
        }

        public int DeleteUserStore(UserStore line)
        {
            return database.Delete(line);
        }
        public int SaveUserStore(UserStore l)
        {
            try
            {
                if (database.Table<UserStore>().Any(xc => xc.UserId == l.UserId))
                {
                    return database.Update(l);
                }
                else
                {
                    return database.Insert(l);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int DeleteUserStoreByUserId(string userId)
        {
            var list = database.Table<UserStore>().Where(a => a.UserId == userId).ToList();
            if (list.Count() == 0)
            {
                return 0;
            }
            list.ForEach((line) => { database.Delete(line); });
            return list.Count();
        }


        public UserStore GetUserStore(string userId)
        {
            return database.Table<UserStore>().Where(a => a.UserId == userId).FirstOrDefault();
        }
        public bool ValidExistUserStore(string userId)
        {
            return database.Table<UserStore>().Any(a => a.UserId == userId);
        }
        public void Dispose()
        {
            database.Dispose();
        }

    }
}
