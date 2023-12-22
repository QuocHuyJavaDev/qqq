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
    public class NoteVM : INote
    {
        public async Task<bool> AddNote(Note note)
        {
            string json = JsonConvert.SerializeObject(note);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Note/addnote");
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

        public async Task<bool> DeleteNote(int noteid)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Note/delete/" + noteid);
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

        public async Task<bool> FavorChange(int noteid, Note note)
        {
            string json = JsonConvert.SerializeObject(note);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Note/favor/" + noteid);
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

        public async Task<List<Note>> GetByNtbId(int ntbid)
        {
            var note = new List<Note>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Note/getbyntb/" + ntbid);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                note = await responseMessage.Content.ReadFromJsonAsync<List<Note>>();

            }
            return await Task.FromResult(note);
        }

        public async Task<List<Note>> GetByUserId(int userid)
        {
            var note = new List<Note>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Note/getbyuser/" + userid);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                note = await responseMessage.Content.ReadFromJsonAsync<List<Note>>();

            }
            return await Task.FromResult(note);
        }

        public async Task<List<Note>> GetFavpr(int isfavor, int userid)
        {
            var note = new List<Note>();
            //
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Note/getfavor/" + isfavor + "/" + userid);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            if (responseMessage.IsSuccessStatusCode)
            {
                note = await responseMessage.Content.ReadFromJsonAsync<List<Note>>();

            }
            return await Task.FromResult(note); 
        }

        public async Task<bool> UpdNote(int noteid, Note note)
        {
            string json = JsonConvert.SerializeObject(note);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Note/editnote/" + noteid);
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
