using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public interface IAccountDao
    {
        public User RegisterAccount(User user);
        public User GetAccount(string email);
        public User GetAccount(int id);
        public void UpdateAccount();
        public void ChangePassword();
    }
}
