using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public interface IWalletDao
    {
        public Wallet CreateWallet(Wallet wallet);
        public List<Wallet> GetAllWallets(int userId);
        public Wallet GetWallet(int id);
        public void UpdateWallet();
        public void DeleteWallet(Wallet wallet);
    }
}
