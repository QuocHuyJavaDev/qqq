using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using MyNote.Models;
using MyNote.Service;
using MyNote.UI.Mobile.ShellMain;
using MyNote.ViewModels;

namespace MyNote.UI.Mobile.PopupView;

public partial class AddNtb : ContentPage
{
	private readonly INotebook _ntbService = new NotebookVM();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public AddNtb()
	{
		InitializeComponent();
	}

    private async void Add_Click(object sender, EventArgs e)
    {
        string ntbName = txtNtbName.Text;
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        int userid = App.userInfor.UserId;
        Notebook ntb = new Notebook
        {
            NotebookName = ntbName,
            DateCreate = getDate,
            ByUser = userid
        };
        bool check = await _ntbService.AddNtb(ntb);
        if (check == true)
        {
            await Navigation.PushAsync(new MbNotebook());
            string text = "Add Successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = "Something went wrong!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private void back_click(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}