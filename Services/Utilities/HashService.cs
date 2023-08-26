using Data.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class HashService
    {
        public string Hash(string password)
        {
            var hash = SHA256.Create();

            var passwordBytes = Encoding.Default.GetBytes(password);
            //encrypt the password
            var hasedPassword = hash.ComputeHash(passwordBytes);

            return Convert.ToHexString(hasedPassword);
        }
    }
}
