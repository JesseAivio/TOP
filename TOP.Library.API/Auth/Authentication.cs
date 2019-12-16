
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TOP.Library.Data.models;

namespace TOP.Library.API.Auth
{
    public class Authentication
    {
        public async Task<Account> LogIn(Account accountParam)
        {
            Account account = null;
            HttpResponseMessage response = await HttpClientSettings.client.PostAsJsonAsync(Url.Action_Authenticate, accountParam);
            if (response.IsSuccessStatusCode)
            {
                account = await response.Content.ReadAsAsync<Account>();
            }
            return account;
        }

        public async Task<bool> Register(Account account)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PostAsJsonAsync(Url.Action_Register, account);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
