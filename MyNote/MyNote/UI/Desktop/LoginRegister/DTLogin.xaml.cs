using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Newtonsoft.Json;
using MyNote.Models;
using MyNote.Service;
using MyNote.UI.Desktop.MainDesktop;
using MyNote.ViewModels;

namespace MyNote.UI.Desktop.LoginRegister;

public partial class DTLogin : ContentPage
{
    private readonly IUser _userService = new UserVM();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public DTLogin()
	{
		InitializeComponent();
	}

    private async void Register_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DTRegister());
    }

    private async void Login_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text;
        string password = txtPassword.Text;
        if (userName == null || password == null)
        {
            string text = "Please input Username & Password";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }
        User userModel = await _userService.Login(userName, password);
        if (Preferences.ContainsKey(nameof(App.userInfor)))
        {
            Preferences.Remove(nameof(App.userInfor));
        }
        string userDetail = JsonConvert.SerializeObject(userModel);
        Preferences.Set(nameof(App.userInfor), userDetail);
        App.userInfor = userModel;
        if (userModel != null)
        {

            await Shell.Current.GoToAsync($"//{nameof(DTHome)}");
        }
        else
        {
            string text = "Username or Password incorrect!";
            //await DisplayAlert("Warning", "Please input Username & Password", "OK");
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private void Forgot_Click(object sender, EventArgs e)
    {

    }
}