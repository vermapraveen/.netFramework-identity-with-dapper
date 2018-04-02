using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YourApplication.DataLayer;
using YourApplication.Models;

namespace YourApplication.UserIdentity
{
    public class YourApplicationUserStore : IUserStore<YourApplicationUser, int>
        , IUserLoginStore<YourApplicationUser, int>
        , IUserPasswordStore<YourApplicationUser, int>
        , IUserSecurityStampStore<YourApplicationUser, int>
        , IUserEmailStore<YourApplicationUser, int>
        , IUserLockoutStore<YourApplicationUser, int>
        , IUserTwoFactorStore<YourApplicationUser, int>
        , IUserRoleStore<YourApplicationUser, int>
        ,IUserClaimStore<YourApplicationUser, int>
    {
        private readonly YourApplicationUserRepository _userRepository;
        private bool _disposed;

        public YourApplicationUserStore(YourApplicationUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Dispose()
        {
            _disposed = true;
        }

        public async Task CreateAsync(YourApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            await _userRepository.AddUserAsync(user);
        }

        public async Task DeleteAsync(YourApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<YourApplicationUser> FindByNameAsync(string userName)
        {
            this.ThrowIfDisposed();

            return await _userRepository.FindLoginByNameAsync(userName);
        }

        public async Task UpdateAsync(YourApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            await _userRepository.UpdateUserAsync(user);
        }

        public Task AddLoginAsync(YourApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<YourApplicationUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(YourApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(YourApplicationUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(YourApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult<string>(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(YourApplicationUser user)
        {
            return Task.FromResult<bool>(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(YourApplicationUser user, string passwordHash)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult<int>(0);
        }

        public Task<string> GetSecurityStampAsync(YourApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<string>(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(YourApplicationUser user, string stamp)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.SecurityStamp = stamp;
            return Task.FromResult<int>(0);
        }

        public Task SetEmailAsync(YourApplicationUser user, string email)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.Email = email;
            return Task.FromResult<int>(0);
        }

        public Task<string> GetEmailAsync(YourApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<string>(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(YourApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(YourApplicationUser user, bool confirmed)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.EmailConfirmed = confirmed;
            return Task.FromResult<int>(0);
        }

        public async Task<YourApplicationUser> FindByEmailAsync(string email)
        {
            this.ThrowIfDisposed();

            return await _userRepository.FindLoginByEmailAsync(email);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(YourApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(YourApplicationUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(YourApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(YourApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            user.AccessFailedCount = 0;
            return Task.FromResult<int>(0);
        }

        public Task<int> GetAccessFailedCountAsync(YourApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<int>(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(YourApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(YourApplicationUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(YourApplicationUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(YourApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult<bool>(user.TwoFactorEnabled);
        }

        public async Task<YourApplicationUser> FindByIdAsync(int userId)
        {
            this.ThrowIfDisposed();

            return await _userRepository.FindLoginByIdAsync(userId);
        }

        public Task AddToRoleAsync(YourApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(YourApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(YourApplicationUser user)
        {
            this.ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var roles = await _userRepository.GetRolesForUserAsync(user.Id);
            return (roles != null) ? roles.ToList() : null;
        }

        public Task<bool> IsInRoleAsync(YourApplicationUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Claim>> GetClaimsAsync(YourApplicationUser user)
        {
            var userDetails = await _userRepository.GetUserDetailsAsync(user.Id);
            IList<Claim> claims = new List<Claim>
            {
                new Claim("FirstName", userDetails.FirstName),
                new Claim("LastName", userDetails.LastName),
                new Claim("HospitalNumber", userDetails.HospitalNumber),
                new Claim("Email", userDetails.Email),
                new Claim("PhoneNumber", userDetails.PhoneNumber),
                new Claim("IsActive", userDetails.IsActive.ToString()),
            };

            return claims;
        }

        public Task AddClaimAsync(YourApplicationUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(YourApplicationUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        private void ThrowIfDisposed()
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }
    }
}
