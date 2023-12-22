using MyNote.Models;
using MyNote.Service;
using MyNote.ViewModels;

namespace MyNote.UI.Mobile.ShellMain;

public partial class MbSetting : ContentPage
{
    private IUserDetail _udSer = new UserDetailVM();
    public MbSetting()
	{
		InitializeComponent();
        GetInfor();
	}
    private async void GetInfor()
    {
        UserDetail ud = await _udSer.GetByUsId(App.userInfor.UserId);
        if (ud != null)
        {
            lblName.Text = ud.UsDetailName;
            lblYear.Text = ud.UsDetailDOB.Split(' ')[0];
            if (ud.UsDetailSex == 1)
            {
                lblSex.Text = "Man";
            }
            else
            {
                lblSex.Text = "Woman";
            }
        }
    }
    private void Logout_Click(object sender, EventArgs e)
    {
        if (Preferences.ContainsKey(nameof(App.userInfor)))
        {
            Preferences.Remove(nameof(App.userInfor));
        }
        Preferences.Clear();
        Application.Current.MainPage = new MbShell();
    }

    private async void UserDetail_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MbUserDetail());
    }
}