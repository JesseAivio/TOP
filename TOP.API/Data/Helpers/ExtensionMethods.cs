using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOP.Library.Data.models;

namespace TOP.API.Data.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<Account> WithoutPasswords(this IEnumerable<Account> accounts)
        {
            if (accounts == null) return null;

            return accounts.Select(x => x.WithoutPassword());
        }

        public static Account WithoutPassword(this Account account)
        {
            if (account == null) return null;

            account.Password = null;
            account.Salt = null;
            return account;
        }
    }
}
