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
    public class UserDetailVM : IUserDetail
    {
        public async Task<bool> AddInfor(UserDetail ud)
        {
            string json = JsonConvert.SerializeObject(ud);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/UserDetail/addDetail");
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

        public async Task<bool> EditInfor(int userid, UserDetail ud)
        {
            string json = JsonConvert.SerializeObject(ud);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/UserDetail/editInfor/" + userid);
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

        public async Task<UserDetail> GetByUsId(int userid)
        {
            var userDetail = new List<UserDetail>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/UserDetail/getbyUser/" + userid);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = responseMessage.Content.ReadAsStringAsync().Result;
                userDetail = JsonConvert.DeserializeObject<List<UserDetail>>(content);
                return await Task.FromResult(userDetail.FirstOrDefault());
            }
            else
            {
                return null;
            }
        }
    }
}
