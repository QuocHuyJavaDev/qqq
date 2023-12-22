using Newtonsoft.Json;
using MyNote.Models;
using MyNote.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.ViewModels
{
    public class ScheduleVM : ISchedule
    {

        public async Task<bool> AddSche(Schedule sche)
        {
            string json = JsonConvert.SerializeObject(sche);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Schedule/addsche");
            HttpResponseMessage responseMessage = await client.PostAsync("", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> EditSche(int scheid, Schedule sche)
        {
            string json = JsonConvert.SerializeObject(sche);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Schedule/editsche/" + scheid);
            HttpResponseMessage responseMessage = await client.PutAsync("", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Schedule>> GetScheList(int userid)
        {
            var sche = new List<Schedule>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Schedule/sche/" + userid);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                sche = await responseMessage.Content.ReadFromJsonAsync<List<Schedule>>();

            }
            return await Task.FromResult(sche);
        }
    }
}
