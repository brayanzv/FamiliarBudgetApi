using Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class GeneralValidator
    {
        private readonly IWalletDao walletDao;
        public GeneralValidator(IWalletDao walletDao) 
        {
            this.walletDao = walletDao;
        }

        public bool existWallet(int userId, int walletId)
        {
            var wallet = walletDao.GetWallet(walletId);

            if (wallet == null)
            {
                return false;
            }

            if (wallet.UserId != userId)
            {
                return false;
            }

            return true;
        }
    }
}
