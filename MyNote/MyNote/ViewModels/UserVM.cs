using Newtonsoft.Json;
using MyNote.Models;
using MyNote.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.ViewModels
{
    public class UserVM : IUser
    {
        public async Task<User> Login(string username, string password)
        {
            var userInfor = new List<User>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/User/" + username + "/" + password);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = responseMessage.Content.ReadAsStringAsync().Result;
                userInfor = JsonConvert.DeserializeObject<List<User>>(content);
                return await Task.FromResult(userInfor.FirstOrDefault());
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Register(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/User/register");
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
    }
}
