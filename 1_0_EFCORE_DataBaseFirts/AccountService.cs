using System;
using System.Collections.Generic;
using System.Linq;
using _1_0_EFCORE_DataBaseFirts.Context;
using _1_0_EFCORE_DataBaseFirts.Models;
using Microsoft.EntityFrameworkCore;

namespace _1_0_EFCORE_DataBaseFirts
{
    public class AccountService
    {
        private DatabaseContext dbContext;
        private List<AccountsAdo> _lstAccountsAdos;
        public AccountService()
        {
            dbContext = new DatabaseContext();// Khoiwi tạo lớp đối tượng kết noi CSDL và có các phươn thức làm ciệc với các bảng
            _lstAccountsAdos = new List<AccountsAdo>();
            getListACCFromDB();
        }
        //Phương thức insret vào table
        public bool AddnewACC(AccountsAdo acc)
        {
            dbContext.AccountsAdos.Add(acc);
            dbContext.SaveChanges();
            return true;
        }
        // phương thức Sửa -- trog table
        public bool UpdateAcc(AccountsAdo acc)
        {
            dbContext.AccountsAdos.Update(acc);
            dbContext.SaveChanges();
            return true;
        }

        public bool DeleteACC(Guid id)
        {
            // đối tượng ACC trong bảng CSDL sau đó tiên hành Xóa
            AccountsAdo acc = dbContext.AccountsAdos.ToList().FirstOrDefault(c => c.Id == id);
            dbContext.AccountsAdos.Remove(acc);
            return true;
        }
        // phương thức tỉm kiếm gần đúng
        public List<AccountsAdo> GetListACCByStartWith(string Acc)
        {
            List<AccountsAdo> temp = dbContext.AccountsAdos.Where(c => c.Acc.StartsWith(Acc)).ToList();
            return temp;
        }
        // Lấy dữ liệu từ Table trong DB
        public void getListACCFromDB()
        {
            _lstAccountsAdos = dbContext.AccountsAdos.AsNoTracking().ToList();
        }

        public List<AccountsAdo> getListACC_Service()
        {
            return _lstAccountsAdos;
        }
        public string[] getYearofBirth()
        {
            string[] temps = new string[2021 - 1900];
            int temp = 1900;
            for (int i = 0; i < temps.Length; i++)
            {
                temps[i] = temp.ToString();
                temp++;

            }

            return temps;
        }

    }
}