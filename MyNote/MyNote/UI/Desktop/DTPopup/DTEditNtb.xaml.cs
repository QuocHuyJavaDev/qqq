using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using MyNote.Models;
using MyNote.Service;
using MyNote.UI.Desktop.MainDesktop;
using MyNote.ViewModels;

namespace MyNote.UI.Desktop.DTPopup;

public partial class DTEditNtb : ContentPage
{
    private readonly INotebook _ntbService = new NotebookVM();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public DTEditNtb()
	{
		InitializeComponent();
        txtNtbName.Text = DTNotebook.ntb2.NotebookName;
    }

    private async void Edit_Click(object sender, EventArgs e)
    {
        string ntbName = txtNtbName.Text;
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        int userid = App.userInfor.UserId;
        int ntbId = DTNotebook.ntb2.NotebookId;
        Notebook notebook = new Notebook
        {
            NotebookName = ntbName,
            DateCreate = getDate,
            ByUser = userid
        };
        bool check = await _ntbService.UpdNtb(ntbId, notebook);
        if (check == true)
        {
            await Navigation.PushAsync(new DTNotebook());
            string text = "Change Successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = "Change Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }
}