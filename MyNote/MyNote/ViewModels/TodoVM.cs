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
    public class TodoVM : ITodo
    {
        public async Task<bool> AddTodo(TodoMain tdMain)
        {
            string json = JsonConvert.SerializeObject(tdMain);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/TDMain/addtodo");
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

        public async Task<bool> DeleteTask(int mainid)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/TDMain/delete/" + mainid);
            HttpResponseMessage responseMessage = await client.DeleteAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return false;
            }
        }

        public async Task<List<TodoMain>> GetMainByUs(int userid)
        {
            var tdl = new List<TodoMain>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/TDMain/" + userid);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                tdl = await responseMessage.Content.ReadFromJsonAsync<List<TodoMain>>();

            }
            return await Task.FromResult(tdl);
        }

        public async Task<bool> NameChange(int mainid, TodoMain tdmain)
        {
            string json = JsonConvert.SerializeObject(tdmain);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/TDMain/editTDName/" + mainid);
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
    }
}
