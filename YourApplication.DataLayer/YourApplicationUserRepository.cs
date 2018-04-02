using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using YourApplication.Models;

namespace YourApplication.DataLayer
{
    public class YourApplicationUserRepository : IDisposable
    {
        private readonly string _connectionString;

        public YourApplicationUserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SQLDbConnection"].ConnectionString;
        }

        public static YourApplicationUserRepository Create()
        {
            return new YourApplicationUserRepository();
        }

        public async Task<bool> AddUserAsync(YourApplicationUser user)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
               var result = await conn.ExecuteAsync(
                    "CreateNewUser"
                    , new
                    {
                        userName = user.UserName,
                        email = user.Email,
                        passwordHash = user.PasswordHash,
                        securityStamp = user.SecurityStamp,
                        roleId = user.RoleId,
                        FName = user.FirstName,
                        MName = user.MiddleName,
                        LName = user.LastName,
                        HNbr = user.HospitalNumber,
                        Gender = user.Gender,
                        Age = user.Age,
                        Dob = user.DOB,
                        Address = user.Address,
                        BGroup = user.BloodGroup,
                        Contact = user.PhoneNumber,
                        AltContact = user.AlternateContactNumber,
                    }
                    , commandType: CommandType.StoredProcedure);

                return result > 0;
            }
        }

        public async Task<bool> UpdateUserAsync(YourApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var result = connection.ExecuteAsync(@"
                    update ApplicationUser set 
                    UserName = @userName, PasswordHash = @passwordHash, SecurityStamp = @securityStamp 
                    where Id = @Id", new { userName = user.UserName, passwordHash = user.PasswordHash, securityStamp = user.SecurityStamp, Id = user.Id });
                return await result > 0;
            }
        }

        public async Task<bool> UpdateUserDetailsAsync(YourApplicationUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var result = connection.ExecuteAsync(@"
                    update UserDetail set 
                    FirstName = @firstName, MiddleName = @middleName, LastName = @lastName 
                    ,Gender = @gender, BloodGroup = @bloodGroup
                    ,DOB = @dob
                    ,ContactNumber = @contactNumber, AlternateContactNumber =@altContactNumber
                    where UserId = @Id", 
                    new {
                        firstName = user.FirstName, middleName = user.MiddleName,
                        lastName = user.LastName, gender = user.Gender,
                        bloodGroup = user.BloodGroup,
                        dob = user.DOB,
                        contactNumber = user.PhoneNumber,
                        altContactNumber = user.AlternateContactNumber,
                        Id = user.Id
                    });
                return await result > 0;
            }
        }

        public async Task<YourApplicationUser> FindLoginByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("userName");

            var sql = @"select [Id]
                                                                        ,[UserName]
                                                                        ,[Email]
                                                                        ,[EmailConfirmed]
                                                                        ,[PasswordHash]
                                                                        ,[SecurityStamp] from ApplicationUser where lower(UserName) = lower(@userName)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<YourApplicationUser>(sql, new { userName });
            }
        }

        public async Task<YourApplicationUser> FindLoginByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            var sql = @"select [Id]
                                                                        ,[UserName]
                                                                        ,[Email]
                                                                        ,[EmailConfirmed]
                                                                        ,[PasswordHash]                                                                        
                                                                        ,[SecurityStamp] from ApplicationUser where lower(Email) = lower(@email)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<YourApplicationUser>(sql, new { email });
            }
        }

        public async Task<YourApplicationUser> FindLoginByIdAsync(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<YourApplicationUser>(@"select [Id]
                                                                        ,[UserName]
                                                                        ,[Email]
                                                                        ,[EmailConfirmed]
                                                                        ,[PasswordHash]                                                                        
                                                                        ,[SecurityStamp] from ApplicationUser where Id = @userId", new { userId = userId });
            }
        }

        public async Task<IEnumerable<string>> GetRolesForUserAsync(int userId)
        {
            string sql = string.Format(
                                    @"SELECT R.Name  FROM ApplicationUser U(NOLOCK) 
                                        LEFT JOIN ApplicationRole R(NOLOCK) ON U.RoleId = R.Id
                                        WHERE U.Id = {0}", userId);

            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<string>(sql);
            }
        }

        public async Task<YourApplicationUser> GetUserDetailsAsync(int userId)
        {
            string sql = string.Format(
                                    @"SELECT 
	                                    U1.Id, U1.Email, FirstName,	MiddleName,	LastName
	                                    ,HospitalNumber, Gender, DOB, Address, BloodGroup
	                                    ,ContactNumber as PhoneNumber, AlternateContactNumber,	IsActive  
                                    FROM ApplicationUser U1 INNER JOIN UserDetail U2 
                                    ON U1.Id = U2.UserId
                                    where U1.Id = {0}", userId);

            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryFirstAsync<YourApplicationUser>(sql);
            }
        }

        public void Dispose()
        {
        }
    }
}
