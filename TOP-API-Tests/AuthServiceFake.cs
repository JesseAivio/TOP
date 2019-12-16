using Konscious.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TOP.API.Data.Helpers;
using TOP.API.Service;
using TOP.Library.Data.models;

namespace TOP_API_Tests
{
    public class AuthServiceFake : IAuthService
    {
        readonly List<Account> accounts;
        public AuthServiceFake()
        {
            accounts = new List<Account>()
            {
                new Account(){Id = Guid.Parse("9d546f1f-09e6-4d95-9102-703d07c1aea7"), Username = "Teacher", Password = "bEX0bscQPUiRXR1QGYTq8Q==", Salt = "ix2fZrdcchxSjCLupvaWOw==", Role = "Teacher", Token = null},
                new Account(){Id = Guid.Parse("9b3e7bc5-eb63-4d65-9556-bd3927251fc4"), Username = "Student", Password = "bEX0bscQPUiRXR1QGYTq8Q==", Salt = "ix2fZrdcchxSjCLupvaWOw==", Role = "Student", Token = null}
            };
        }
        public Account Authenticate(string username, string password)
        {
            var account = accounts.SingleOrDefault(x => x.Username == username);
            if (account == null || VerifyHash(password, Convert.FromBase64String(account.Salt), Convert.FromBase64String(account.Password)) == false)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("bgw9834bjugfnigrh83w");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Id.ToString()),
                    new Claim(ClaimTypes.Role, account.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            account.Token = tokenHandler.WriteToken(token);

            return account.WithoutPassword();
        }

        public bool Register(Account accountParam)
        {
            var account = accounts.SingleOrDefault(x => x.Username == accountParam.Username);

            if (account != null)
                return false;

            var salt = CreateSalt();
            var password = HashPassword(accountParam.Password, salt);
            account = new Account
            {
                Username = accountParam.Username,
                Salt = Convert.ToBase64String(salt),
                Password = Convert.ToBase64String(password),
                Role = accountParam.Role
            };

            accounts.Add(account);
            return true;
        }

        public Account GetAccount(Guid accountId)
        {
            var account = accounts.SingleOrDefault(x => x.Id == accountId);

            return account.WithoutPassword();
        }

        public IEnumerable<Account> GetAccounts()
        {
            return accounts.WithoutPasswords();
        }

        public void DeleteAccount(string username)
        {
            var account = accounts.SingleOrDefault(x => x.Username == username);
            accounts.Remove(account);
        }
        #region Argon2
        private bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash);
        }
        private byte[] CreateSalt()
        {
            var buffer = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(buffer);
            }
            return buffer;
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8, // four cores
                Iterations = 4,
                MemorySize = 1024 * 1024 // 1 GB
            })
            {
                return argon2.GetBytes(16);
            }
        }
        #endregion
    }
}
