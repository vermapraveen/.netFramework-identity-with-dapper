using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace YourApplication.Models
{
    public class YourApplicationUser : IUser<int>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<YourApplicationUser, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string HospitalNumber { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public string AlternateContactNumber { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
