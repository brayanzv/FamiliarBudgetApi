using Data.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class CurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public User GetCurrentUser()
        {
            var currentUser = httpContextAccessor.HttpContext.User;

            var CurrentUserRoleId = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value != ""
                ? currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value : "0";

            var currentUserFamilyId = currentUser.Claims.FirstOrDefault(x => x.Type == "familyId").Value != ""
                ? currentUser.Claims.FirstOrDefault(x => x.Type == "familyId").Value : "0";

            return new User()
            {
                UserId = Int32.Parse(currentUser.Claims.FirstOrDefault(x => x.Type == "userId").Value),
                FirstName = currentUser.Claims.FirstOrDefault(x => x.Type == "firstName").Value,
                LastName = currentUser.Claims.FirstOrDefault(x => x.Type == "lastName").Value,
                Avatar = currentUser.Claims.FirstOrDefault(x => x.Type == "avatar").Value,
                Email = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
                RoleId = Int32.Parse(CurrentUserRoleId),
                FamilyId = Int32.Parse(currentUserFamilyId)
            };
        }
    }
}
