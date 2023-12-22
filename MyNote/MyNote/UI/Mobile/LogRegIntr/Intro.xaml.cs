namespace MyNote.UI.Mobile.LogRegIntr;

public partial class Intro : ContentPage
{
	public Intro()
	{
		InitializeComponent();
	}

    private async void Register_Click(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 0.8;
        await Navigation.PushAsync(new MbRegister());
        (sender as Button).Opacity = 1;
    }

    private async void Login_Click(object sender, EventArgs e)
    {
        (sender as Button).Opacity = 0.8;
        await Navigation.PushAsync(new MbLogin());
        (sender as Button).Opacity = 1;
    }
}