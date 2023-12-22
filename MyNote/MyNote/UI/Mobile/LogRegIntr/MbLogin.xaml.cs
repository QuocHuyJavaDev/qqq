using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Newtonsoft.Json;
using MyNote.Models;
using MyNote.Service;
using MyNote.UI.Mobile.ShellMain;
using MyNote.ViewModels;

namespace MyNote.UI.Mobile.LogRegIntr;

public partial class MbLogin : ContentPage
{
    private readonly IUser _userService = new UserVM();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public MbLogin()
	{
		InitializeComponent();
	}

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void Forgot_Click(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 0.8;
        await Navigation.PushAsync(new MbForgot());
        (sender as Button).Opacity = 1;
    }

    private async void Login_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text;
        string password = txtPassword.Text;
        if (userName == null || password == null)
        {
            string text = "Please input Username & Password";
            //await DisplayAlert("Warning", "Please input Username & Password", "OK");
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

            await Shell.Current.GoToAsync($"//{nameof(MbHome)}");
        }
        else
        {
            string text = "Username or Password incorrect!";
            //await DisplayAlert("Warning", "Please input Username & Password", "OK");
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void Register_Click(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 0.8;
        await Navigation.PushAsync(new MbRegister());
        (sender as Button).Opacity = 1;
    }
}