using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TOP.Library.Data.models;

namespace TOP.Library.API.Models
{
    public class Accounts_Functionality
    {
        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            IEnumerable<Account> accounts = null;
            HttpResponseMessage response = await HttpClientSettings.client.GetAsync(Url.Controller_Auth);
            if (response.IsSuccessStatusCode)
            {
                accounts = await response.Content.ReadAsAsync<IEnumerable<Account>>();
            }
            return accounts;
        }

        public async Task<string> RegisterAccountAsync(Account account)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PostAsJsonAsync(Url.Action_Register, account);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> UpdateAccountAsync(Account account)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PutAsJsonAsync(Url.Controller_Auth, account);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> DeleteAccountAsync(string Username)
        {
            string requestUri = Url.Controller_Auth + "/" + Username;
            HttpResponseMessage response = await HttpClientSettings.client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
