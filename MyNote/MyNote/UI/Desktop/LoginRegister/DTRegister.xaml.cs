using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyNote.Models;
using MyNote.Service;
using MyNote.ViewModels;

namespace MyNote.UI.Desktop.LoginRegister;

public partial class DTRegister : ContentPage
{
    private readonly IUser _userSevice = new UserVM();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public static User usdata1;
    public DTRegister()
	{
		InitializeComponent();
	}

    private async void Signin_Click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void Register_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text;
        string password = txtPassword.Text;
        string rePassword = txtRePassword.Text;
        if (userName == null || password == null || rePassword == null)
        {
            string text = "Please input Username & Password";
            //await DisplayAlert("Warning", "Please input Username & Password", "OK");
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }
        if (password != rePassword)
        {
            string text = "Password does not match!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }
        usdata1 = new User
        {
            UserName = userName,
            UserPass = password
        };
        await Navigation.PushAsync(new DTAddInfor());
        string text1 = "Next, please add your information!";
        var toast1 = Toast.Make(text1, duration, fontSize);
        await toast1.Show(cancellationTokenSource.Token);
    }
}