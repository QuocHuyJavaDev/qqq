namespace MyNote.UI.Desktop.MainDesktop;

public partial class DTHome : ContentPage
{
	public DTHome()
	{
		InitializeComponent();
        TxtUserName.Text = App.userInfor.UserName;
    }
}