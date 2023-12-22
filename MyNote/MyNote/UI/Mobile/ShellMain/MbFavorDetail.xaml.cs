using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyNote.Models;
using MyNote.Service;
using MyNote.ViewModels;

namespace MyNote.UI.Mobile.ShellMain;

public partial class MbFavorDetail : ContentPage
{
    private INote _noteSer = new NoteVM();
    private bool isChange = false;
    private string oldTitle;
    private string newTitle;
    private string oldDetail;
    private string newDetail;
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public MbFavorDetail()
	{
		InitializeComponent();
        this.BindingContext = new NoteFavorVM();
        oldTitle = entryTit.Text;
        oldDetail = TextEditor.Text;
        int isFavor = MbHome.noteData.IsFavor;
        if (isFavor == 0)
        {
            btnFavor.IsEnabled = false;
            btnFavor.IsVisible = false;
            btnUnfavor.IsEnabled = true;
            btnUnfavor.IsVisible = true;
        }
        else if (isFavor == 1)
        {
            btnFavor.IsEnabled = true;
            btnFavor.IsVisible = true;
            btnUnfavor.IsEnabled = false;
            btnUnfavor.IsVisible = false;
        }
    }
    public static Note createModel()
    {
        Note note = new Note
        {
            NoteName = MbHome.noteData.NoteName,
            NoteDetail = MbHome.noteData.NoteDetail,
            DateAddUp = MbHome.noteData.DateAddUp,
            IsFavor = MbHome.noteData.IsFavor,
            NByNtb = MbHome.noteData.NByNtb,
            NByUser = MbHome.noteData.NByUser
        };
        return note;
    }

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void Save_Click(object sender, EventArgs e)
    {
        string nName = entryTit.Text;
        string nDetail = TextEditor.Text;
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        int userid = App.userInfor.UserId;
        int ntbId = MbHome.noteData.NByNtb;
        int noteid = MbHome.noteData.NoteId;
        int favorr = MbHome.noteData.IsFavor;
        Note note = new Note
        {
            NoteName = nName,
            NoteDetail = nDetail,
            DateAddUp = getDate,
            IsFavor = favorr,
            NByNtb = ntbId,
            NByUser = userid
        };
        bool check = await _noteSer.UpdNote(noteid, note);
        if (check == true)
        {

            string text = "Saved";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = "Failed";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void Unfavo_Click(object sender, EventArgs e)
    {
        int noteid = MbHome.noteData.NoteId;
        Note note = new Note
        {
            NoteName = "",
            NoteDetail = "",
            DateAddUp = "",
            IsFavor = 1,
            NByNtb = 0,
            NByUser = 0
        };
        bool check = await _noteSer.FavorChange(noteid, note);
        if (check == true)
        {
            string text = "Favorited!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            btnFavor.IsEnabled = true;
            btnFavor.IsVisible = true;
            btnUnfavor.IsEnabled = false;
            btnUnfavor.IsVisible = false;
        }
        else
        {
            string text = "Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void Favo_Click(object sender, EventArgs e)
    {
        int noteid = MbHome.noteData.NoteId;
        Note note = new Note
        {
            NoteName = "",
            NoteDetail = "",
            DateAddUp = "",
            IsFavor = 0,
            NByNtb = 0,
            NByUser = 0
        };
        bool check = await _noteSer.FavorChange(noteid, note);
        if (check == true)
        {
            btnFavor.IsEnabled = false;
            btnFavor.IsVisible = false;
            btnUnfavor.IsEnabled = true;
            btnUnfavor.IsVisible = true;
            string text = "Unfavorited!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);

        }
        else
        {
            string text = "Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }
}