using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TOP.API.Data.Context;
using TOP.API.Data.Enteties;
using TOP.API.Data.Helpers;
using TOP.Library.Data.models;

namespace TOP.API.Service
{
    public interface IAuthService
    {
        Account Authenticate(string username, string password);
        bool Register(Account accountParam);
        Account GetAccount(Guid accountId);
        IEnumerable<Account> GetAccounts();
        void DeleteAccount(string username);
        void UpdateAccount(Account account);
    }
    public class AuthService : IAuthService
    {
        readonly TOPContext _topContext;
        readonly AppSettings _appSettings;
        readonly List<Account> _accounts = new List<Account>();//Azure testing
        public AuthService(TOPContext topContext, IOptions<AppSettings> appSettings)
        {
            _topContext = topContext;
            _appSettings = appSettings.Value;
            AddAzureAccounts();
        }

        private void AddAzureAccounts()
        {
            Account Test = new Account()
            {
                Id = Guid.NewGuid(),
                Username = "Test",
                Password = "iWqGnt0XM/N/EsdDLWuPwA==",
                Salt = "tJ+GMJN8p2XlxDONodAATA==",
                Role = Role.Student//Test, Test
            };
            Account admin = new Account()
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Password = "Pb0eB3+qJOsP2iPS/Ch0Ug==",
                Salt = "c9z+qUsElDw4i0tQ9kvqPw==",
                Role = Role.Teacher//admin, admin
            };
            _accounts.Add(Test);
            _accounts.Add(admin);
        }
        public Account Authenticate(string username, string password)
        {
            Account account = new Account();
            SecurityToken token = null;
            SecurityTokenHandler tokenHandler = null;
            byte[] key = null;
            SecurityTokenDescriptor tokenDescriptor = null;
            if (AppSettings.isAzure)
            {
                account = _accounts.FirstOrDefault(x => x.Username == username);
                if (account == null || VerifyHash(password, Convert.FromBase64String(account.Salt), Convert.FromBase64String(account.Password)) == false)
                    return null;

                tokenHandler = new JwtSecurityTokenHandler();
                key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, account.Id.ToString()),
                    new Claim(ClaimTypes.Role, account.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                token = tokenHandler.CreateToken(tokenDescriptor);
                account.Token = tokenHandler.WriteToken(token);

                return account.WithoutPassword();
            }

            account = _topContext.Accounts.FirstOrDefault(x => x.Username == username);
            if (account == null || VerifyHash(password, Convert.FromBase64String(account.Salt), Convert.FromBase64String(account.Password)) == false)
                return null;

            tokenHandler = new JwtSecurityTokenHandler();
            key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Id.ToString()),
                    new Claim(ClaimTypes.Role, account.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            token = tokenHandler.CreateToken(tokenDescriptor);
            account.Token = tokenHandler.WriteToken(token);

            return account.WithoutPassword();
        }

        public bool Register(Account accountParam)
        {
            Account account = new Account();
            byte[] salt = null;
            byte[] password = null;
            if (AppSettings.isAzure)
            {
                account = _accounts.FirstOrDefault(x => x.Username == accountParam.Username);

                if (account != null)
                    return false;

                salt = CreateSalt();
                password = HashPassword(accountParam.Password, salt);
                account = new Account
                {
                    Id = Guid.NewGuid(),
                    Username = accountParam.Username,
                    Salt = Convert.ToBase64String(salt),
                    Password = Convert.ToBase64String(password),
                    Role = accountParam.Role
                };

                _accounts.Add(account);
                return true;
            }

            account = _topContext.Accounts.FirstOrDefault(x => x.Username == accountParam.Username);

            if (account != null)
                return false;
 
            salt = CreateSalt();
            password = HashPassword(accountParam.Password, salt);
            account = new Account
            {
                Username = accountParam.Username,
                Salt = Convert.ToBase64String(salt),
                Password = Convert.ToBase64String(password),
                Role = accountParam.Role
            };

            _topContext.Accounts.Add(account);
            _topContext.SaveChanges();
            return true;
        }

        public Account GetAccount(Guid accountId)
        {
            Account account = new Account();
            if (AppSettings.isAzure)
            {
                account = _accounts.FirstOrDefault(x => x.Id == accountId);

                return account.WithoutPassword();
            }
            account = _topContext.Accounts.FirstOrDefault(x => x.Id == accountId);

            return account.WithoutPassword();
        }

        public IEnumerable<Account> GetAccounts()
        {
            if (AppSettings.isAzure)
            {
                return _accounts.ToList().WithoutPasswords();
            }
            return _topContext.Accounts.ToList().WithoutPasswords();
        }

        public void DeleteAccount(string username)
        {
            Account account = new Account();
            if (AppSettings.isAzure)
            {
                account = _accounts.FirstOrDefault(x => x.Username == username);
                _accounts.Remove(account);
            }
            account = _topContext.Accounts.FirstOrDefault(x => x.Username == username);
            _topContext.Accounts.Remove(account);
            _topContext.SaveChanges();
        }

        public void UpdateAccount(Account account)
        {
            Account dbAccount = new Account();
            if (AppSettings.isAzure)
            {
                dbAccount = _accounts.FirstOrDefault(x => x.Id == account.Id);
                if (account.Password != "")
                {
                    if (VerifyHash(account.Password, Convert.FromBase64String(dbAccount.Salt), Convert.FromBase64String(dbAccount.Password)) == false)
                    {
                        var salt = CreateSalt();
                        var password = HashPassword(account.Password, salt);
                        dbAccount.Password = Convert.ToBase64String(password);
                        dbAccount.Salt = Convert.ToBase64String(salt);
                    }
                }
                if (_accounts.FirstOrDefault(x => x.Username == account.Username) == null)
                    dbAccount.Username = account.Username;

                dbAccount.Role = account.Role;
            }

            dbAccount = _topContext.Accounts.FirstOrDefault(x => x.Id == account.Id);
            if(account.Password != "")
            {
                if (VerifyHash(account.Password, Convert.FromBase64String(dbAccount.Salt), Convert.FromBase64String(dbAccount.Password)) == false)
                {
                    var salt = CreateSalt();
                    var password = HashPassword(account.Password, salt);
                    dbAccount.Password = Convert.ToBase64String(password);
                    dbAccount.Salt = Convert.ToBase64String(salt);
                }
            }
            if(_topContext.Accounts.FirstOrDefault(x => x.Username == account.Username) == null)
                dbAccount.Username = account.Username;

            dbAccount.Role = account.Role;
            _topContext.SaveChanges();
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
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8, // four cores
                Iterations = 4,
                MemorySize = 1024 * 1024 // 1 GB
            };
            if (AppSettings.isAzure)
            {
                argon2.MemorySize = 100 * 100;
            }
            return argon2.GetBytes(16);
        }
        #endregion
    }
}
