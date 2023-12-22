using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Text.RegularExpressions;
using MyNote.Models;
using MyNote.Service;
using MyNote.ViewModels;

namespace MyNote.UI.Mobile.LogRegIntr;

public partial class MbInfor : ContentPage
{
    private readonly IUser _userSevice = new UserVM();
    private readonly IUserDetail _udSevice = new UserDetailVM();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public MbInfor()
	{
		InitializeComponent();
		txtUserName.Text = MbRegister.usdata.UserName;
	}

    private async void Add_Click(object sender, EventArgs e)
    {
        string fName = txtUserName.Text;
        string mail = txtmail.Text;
        string dateOfBirth = dpDOB.Date.ToString();
        if (fName == null || mail == null || dateOfBirth == null)
        {
            string text = "Please input data";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }
        Regex reg = new Regex(@"^([a-zA-Z0-9]+[a-zA-Z0-9\.]*[a-zA-Z0-9]+)@(gmail)\.(com)$");
        bool checkMail = reg.IsMatch(mail);
        if (!checkMail)
        {
            string text = "Email illegal";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            return;
        }
        int sex = -1;
        if (radbtnMan.IsChecked)
        {
            sex = int.Parse(radbtnMan.Value.ToString());
        }
        if (radbtnWman.IsChecked)
        {
            sex = int.Parse(radbtnWman.Value.ToString());
        }
        bool checkRegis = await _userSevice.Register(MbRegister.usdata);
        if (checkRegis == true)
        {
            User userModel = await _userSevice.Login(MbRegister.usdata.UserName, MbRegister.usdata.UserPass);
            if (userModel != null)
            {
                UserDetail user = new UserDetail
                {
                    UsDetailName = fName,
                    UsDetailMail = mail,
                    UsDetailDOB = dateOfBirth,
                    UsDetailSex = sex,
                    UDByUser = userModel.UserId
                };
                bool checkInfor = await _udSevice.AddInfor(user);
                if (checkInfor == true)
                {
                    await Navigation.PushAsync(new Intro());
                    string text = "Register Successfully!";
                    var toast = Toast.Make(text, duration, fontSize);
                    await toast.Show(cancellationTokenSource.Token);
                }
                else
                {
                    string text = "Something went wrong!";
                    var toast = Toast.Make(text, duration, fontSize);
                    await toast.Show(cancellationTokenSource.Token);
                }
            } else
            {
                string text = "Something went wrong!";
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show(cancellationTokenSource.Token);
            }
            
        }
        else
        {
            string text = "Something went wrong!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        
    }

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}