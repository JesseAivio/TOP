using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TOP.Library.Data.models;

namespace TOP.Library.API.Models
{
    public class Teacher_Functionality
    {
        public async Task<IEnumerable<Teacher>> GetTeachersAsync()
        {
            IEnumerable<Teacher> teachers = null;
            HttpResponseMessage response = await HttpClientSettings.client.GetAsync(Url.Action_Teacher);
            if (response.IsSuccessStatusCode)
            {
                teachers = await response.Content.ReadAsAsync<IEnumerable<Teacher>>();
            }
            return teachers;
        }

        public async Task<Teacher> AddTeacherAsync(Teacher teacher)
        {
            Teacher Teacher = null;
            HttpResponseMessage response = await HttpClientSettings.client.PostAsJsonAsync(Url.Action_Teacher, teacher);
            if (response.IsSuccessStatusCode)
            {
                Teacher = await response.Content.ReadAsAsync<Teacher>();
            }
            return Teacher;
        }

        public async Task<string> UpdateTeacherAsync(Teacher teacher)
        {
            HttpResponseMessage response = await HttpClientSettings.client.PutAsJsonAsync(Url.Action_Teacher, teacher);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> DeleteTeacherAsync(Teacher teacher)
        {
            string requestUri = Url.Action_Teacher + "/" + teacher.Id;
            HttpResponseMessage response = await HttpClientSettings.client.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();

            string result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
