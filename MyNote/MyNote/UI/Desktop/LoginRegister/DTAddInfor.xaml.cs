using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Text.RegularExpressions;
using MyNote.Models;
using MyNote.Service;
using MyNote.ViewModels;

namespace MyNote.UI.Desktop.LoginRegister;

public partial class DTAddInfor : ContentPage
{
    private readonly IUser _userSevice = new UserVM();
    private readonly IUserDetail _udSevice = new UserDetailVM();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public DTAddInfor()
	{
		InitializeComponent();
        txtFullName.Text = DTRegister.usdata1.UserName;
    }

    private async void Update_Click(object sender, EventArgs e)
    {
        string fName = txtFullName.Text;
        string mail = txtEmail.Text;
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
        bool checkRegis = await _userSevice.Register(DTRegister.usdata1);
        if (checkRegis == true)
        {
            User userModel = await _userSevice.Login(DTRegister.usdata1.UserName, DTRegister.usdata1.UserPass);
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
                    await Navigation.PushAsync(new DTLogin());
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
            }
            else
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
}