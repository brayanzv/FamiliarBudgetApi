using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public class AccountDao : IAccountDao
    {
        private readonly ApplicationDbContext context;
        public AccountDao(ApplicationDbContext context) { 
            this.context = context;
        }

        public void ChangePassword()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetAccount(string email)
        {
            try
            {
                return context.Users.FirstOrDefault(u => u.Email == email);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public User GetAccount(int id)
        {
            try
            {
                return context.Users.FirstOrDefault(u => u.UserId == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User RegisterAccount(User user)
        {
            try
            {
                var userEntity = context.Users.Add(user);
                context.SaveChanges();

                return userEntity.Entity;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateAccount()
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
