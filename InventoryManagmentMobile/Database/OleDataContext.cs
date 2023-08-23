﻿using InventoryManagmentMobile.Models;
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
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "oleDb.db");
            database = new SQLiteConnection(dbPath);
            database.CreateTable<TransactionLine>();
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

        public int DeleteTransationLine(TransactionLine line)
        {
            return database.Delete(line);
        }

        public int DeleteTransationLineByOrderNo(string OrderNo)
        {
            return database.Execute($"DELETE FROM TransactionLine Where  OrderNo ='{OrderNo}'");
        }
        public int ExecuteSql(string sql)
        {
            return database.Execute(sql);
        }
        public List<TransactionLine> QueryAccountWithPositiveBalance()
        {
            return database.Query<TransactionLine>("SELECT * FROM Account WHERE Balance > 0");
        }

        public List<TransactionLine> GetTransactionLinesByOrderNo(string orderNo)
        {
            return database.Table<TransactionLine>().Where(a => a.OrderNo == orderNo).ToList();
        }
        public TransactionLine GetTransactionLinesByProductNo(string ProductNo)
        {
            return database.Table<TransactionLine>().Where(a => a.ProductBarCode == ProductNo).FirstOrDefault();
        }
        public void Dispose()
        {
            database.Dispose();
        }

    }
}