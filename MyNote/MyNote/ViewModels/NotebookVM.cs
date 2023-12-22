using Newtonsoft.Json;
using MyNote.Models;
using MyNote.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.ViewModels
{
    public class NotebookVM : INotebook
    {
        #region Properities
        //List luu cac NTB
        public ObservableCollection<Notebook> ntbList { get; set; } = new ObservableCollection<Notebook>();
        #endregion
        public NotebookVM() { NtbList(); }
        //
        private async void NtbList()
        {
            ntbList.Clear();
            int userid = App.userInfor.UserId;
            List<Notebook> list = await GetByUsId(userid);
            for (int i = list.Count-1; i >= 0; i--)
            {
                ntbList.Add(list[i]);
            }
            sortList();
        }
        private void sortList()
        {
            for (int i = 0; i < ntbList.Count-1; i++)
            {
                for (int j = i; j< ntbList.Count; j++) 
                {
                    if (ntbList[i].NotebookId < ntbList[j].NotebookId)
                    {
                        Notebook temp = ntbList[i];
                        ntbList[i] = ntbList[j];
                        ntbList[j] = temp;
                    }
                }
            }
        }

        public async Task<bool> AddNtb(Notebook ntb)
        {
            string json = JsonConvert.SerializeObject(ntb);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Ntb/addntb");
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

        //Doc du lieu va lay ve tu WEB API
        public async Task<List<Notebook>> GetByUsId(int userid)
        {
            //Tao list ntb de chua cac ntn
            var ntb = new List<Notebook>();
            //Ket noi vs webapi
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Ntb/" + userid);
            HttpResponseMessage responseMessage = await client.GetAsync("");
            
            if (responseMessage.IsSuccessStatusCode)
            {
                //ReadFromJsonAsync - doc du lieu tu web api xuong
                //cho vao list ntb
                ntb = await responseMessage.Content.ReadFromJsonAsync<List<Notebook>>();

            }
            //return list ntb
            return await Task.FromResult(ntb);
        }

        public async Task<bool> UpdNtb(int ntbId, Notebook ntb)
        {
            string json = JsonConvert.SerializeObject(ntb);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Ntb/" + ntbId);
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

        public async Task<bool> DeleteNtb(int ntbId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl.strUrl + "/api/Ntb/delete/" + ntbId);
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
    }
}
