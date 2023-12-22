using MyNote.Service;
using MyNote.ViewModels;
using MyNote.Models;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MyNote.UI.Desktop.MainDesktop;

public partial class DTAccount : ContentPage
{
    private IUserDetail _udSer = new UserDetailVM();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public DTAccount()
	{
		InitializeComponent();
        GetInfor();
	}
    private async void GetInfor()
    {
        UserDetail ud = await _udSer.GetByUsId(App.userInfor.UserId);
        if (ud != null)
        {
            txtFullName.Text = ud.UsDetailName;
            txtEmail.Text = ud.UsDetailMail;
            txtYear.Text = ud.UsDetailDOB;
            if (ud.UsDetailSex == 1)
            {
                radbtnMan.IsChecked = true;
            } else
            {
                radbtnWman.IsChecked = true;
            }
        }
    }
    private void Signout_Click(object sender, EventArgs e)
    {
        if (Preferences.ContainsKey(nameof(App.userInfor)))
        {
            Preferences.Remove(nameof(App.userInfor));
        }
        Preferences.Clear();
        Application.Current.MainPage = new ShellBase();
    }

    private void Update_Click(object sender, EventArgs e)
    {
        txtFullName.IsEnabled= true;
        txtEmail.IsEnabled = true;
        txtYear.IsEnabled = true;
        radbtnMan.IsEnabled = true;
        radbtnWman.IsEnabled = true;
    }

    private async void Save_Click(object sender, EventArgs e)
    {
        string fName = txtFullName.Text;
        string mail = txtEmail.Text;
        string dateOfBirth = txtYear.Text;
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
            string text = "Edit Successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            txtFullName.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtYear.IsEnabled = false;
            radbtnMan.IsEnabled = false;
            radbtnWman.IsEnabled = false;
        }
        else
        {
            string text = "Something went wrong!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }  
    }
}