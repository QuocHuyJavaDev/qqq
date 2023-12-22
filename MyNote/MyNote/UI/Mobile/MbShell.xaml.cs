using MyNote.UI.Mobile.ShellMain;

namespace MyNote.UI.Mobile;

public partial class MbShell : Shell
{
	public MbShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MbHome), typeof(MbHome));
       // Routing.RegisterRoute(nameof(MbNotebook), typeof(MbNotebook));
    }
}