using MyNote.Service;
using MyNote.ViewModels;
using MyNote.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MyNote.UI.Mobile.ShellMain;

public partial class MbUserDetail : ContentPage
{
    private IUserDetail _udSer = new UserDetailVM();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public MbUserDetail()
	{
		InitializeComponent();
        GetInfor();

    }
    private async void GetInfor()
    {
        UserDetail ud = await _udSer.GetByUsId(App.userInfor.UserId);
        if (ud != null)
        {
            txtUserName.Text = ud.UsDetailName;
            txtmail.Text = ud.UsDetailMail;
            dpDOB.Date = DateTime.Parse(ud.UsDetailDOB);
            if (ud.UsDetailSex == 1)
            {
                radbtnMan.IsChecked = true;
            }
            else
            {
                radbtnWman.IsChecked = true;
            }
        }
    }
    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void Save_Click(object sender, EventArgs e)
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
        UserDetail user = new UserDetail
        {
            UsDetailId = 0,
            UsDetailName = fName,
            UsDetailMail = mail,
            UsDetailDOB = dateOfBirth,
            UsDetailSex = sex
        };
        bool check = await _udSer.EditInfor(App.userInfor.UserId, user);
        if (check == true)
        {
            await Navigation.PushAsync(new MbSetting());
            string text = "Update Successfully!";
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
}