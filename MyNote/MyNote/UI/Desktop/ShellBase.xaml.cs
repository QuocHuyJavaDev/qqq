using MyNote.UI.Desktop.MainDesktop;

namespace MyNote.UI.Desktop;

public partial class ShellBase : Shell
{
	public ShellBase()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(DTHome), typeof(DTHome));
    }
}