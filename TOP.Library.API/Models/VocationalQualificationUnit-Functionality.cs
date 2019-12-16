using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TOP.Library.Data.models;

namespace TOP.Library.API.Models
{
    public class VocationalQualificationUnit_Functionality
    {
        public async Task<IEnumerable<VocationalQualificationUnit>> GetVocationalQualificationUnitsAsync()
        {
            IEnumerable<VocationalQualificationUnit> vocationalQualificationUnits = null;
            HttpResponseMessage response = await HttpClientSettings.client.GetAsync(Url.Action_VocationalQualificationUnit);
            if (response.IsSuccessStatusCode)
            {
                vocationalQualificationUnits = await response.Content.ReadAsAsync<IEnumerable<VocationalQualificationUnit>>();
            }
            return vocationalQualificationUnits;
        }

        public async Task<VocationalQualificationUnit> AddVocationalQualificationUnitAsync(VocationalQualificationUnit vocationalQualificationUnit)
        {
            VocationalQualificationUnit VocationalQualificationUnit = null;
            HttpResponseMessage response = await HttpClientSettings.client.PostAsJsonAsync(Url.Action_VocationalQualificationUnit, vocationalQualificationUnit);
            if (response.IsSuccessStatusCode)
            {
                VocationalQualificationUnit = await response.Content.ReadAsAsync<VocationalQualificationUnit>();
            }
            return VocationalQualificationUnit;
        }

        public async Task<string> UpdateVocationalQualificationUnitAsync(VocationalQualificationUnit vocationalQualificationUnit)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PutAsJsonAsync(Url.Action_VocationalQualificationUnit, vocationalQualificationUnit);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> DeleteVocationalQualificationUnitAsync(VocationalQualificationUnit vocationalQualificationUnit)
        {
            string requestUri = Url.Action_VocationalQualificationUnit + "/" + vocationalQualificationUnit.Id;
            HttpResponseMessage response = await HttpClientSettings.client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
