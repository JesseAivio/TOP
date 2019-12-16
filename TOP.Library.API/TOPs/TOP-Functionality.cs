using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TOP.Library.API.Enteties;

namespace TOP.Library.API.TOPs
{
    public class TOP_Functionality
    {
        public async Task<IEnumerable<TOPModel>> GetTOPSAsync()
        {
            IEnumerable<TOPModel> TOPs = null;
            HttpResponseMessage response = await HttpClientSettings.client.GetAsync(Url.Controller_TOP);
            if (response.IsSuccessStatusCode)
            {
                TOPs = await response.Content.ReadAsAsync<IEnumerable<TOPModel>>();
            }
            return TOPs;
        }

        public async Task<string> UpdateTOPAsync(TOPModel top)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PutAsJsonAsync(Url.Controller_TOP, top);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> DeleteTOPAsync(TOPModel top)
        {
            string requestUri = Url.Controller_TOP + "/" + top.Id;
            HttpResponseMessage response = await HttpClientSettings.client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> AddTOPAsync(TOPModel top)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PostAsJsonAsync(Url.Controller_TOP, top);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
