using Data.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public class WalletDao : IWalletDao
    {
        private readonly ApplicationDbContext context;

        public WalletDao(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Wallet CreateWallet(Wallet wallet)
        {
            try
            {
                var walletEntity = context.Wallets.Add(wallet);
                context.SaveChanges();
                return walletEntity.Entity;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteWallet(Wallet wallet)
        {
            try
            {
                var query = "EXEC DeleteWalletById @WalletId";
                var walletDeleted = context.Database.ExecuteSqlRaw(query, new SqlParameter("@WalletId", wallet.WalletId));
                context.SaveChanges();

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Wallet> GetAllWallets(int userId)
        {
            try
            {
                var walletEntityList = context.Wallets.Where(x => x.UserId == userId).ToList();
                return walletEntityList;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public Wallet GetWallet(int id)
        {
            try
            {
                var walletEntity = context.Wallets.FirstOrDefault(w => w.WalletId == id);

                return walletEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateWallet()
        {
            try
            {
                context.SaveChanges();
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
