using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using MyNote.Models;
using MyNote.Service;
using MyNote.ViewModels;
using System.Collections.ObjectModel;

namespace MyNote.UI.Mobile.ShellMain;

public partial class MbHome : ContentPage
{
    private readonly INote _noteService = new NoteVM();
    private readonly ITodo _todoService = new TodoVM();
    public static ObservableCollection<Note> favorNote { get; set; }
    public static Note noteData;

    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;

    public MbHome()
	{
		InitializeComponent();
        lblName.Text = App.userInfor.UserName + "!";
        favorNote = new ObservableCollection<Note>();
        NoteByNtb();
        FavorView.ItemsSource= favorNote;
    }
    private async void NoteByNtb()
    {
        favorNote.Clear();
        int userid = App.userInfor.UserId;
        List<Note> list = await _noteService.GetFavpr(1, userid);
        for (int i = list.Count - 1; i >= 0; i--)
        {
            favorNote.Add(list[i]);
        }

    }

    private async void AddNote_Click(object sender, EventArgs e)
    {
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        int ntbid = 0;
        int userid = App.userInfor.UserId;
        Note note = new Note
        {
            NoteName = "Title",
            NoteDetail = "Write more...",
            DateAddUp = getDate,
            IsFavor = 0,
            NByNtb = ntbid,
            NByUser = userid
        };
        bool check = await _noteService.AddNote(note);
        if (check == true)
        {
            string text = "Add Note Successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = "Add Note Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void AddTodo_Click(object sender, EventArgs e)
    {
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        int userid = App.userInfor.UserId;
        TodoMain todoMain = new TodoMain
        {
            MainName = "Title",
            DateMain = getDate,
            MByUser = userid
        };
        bool check = await _todoService.AddTodo(todoMain);
        if (check == true)
        {
            //await Navigation.PushAsync(new MbTodo());
            string text = "Add Successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = "Add Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void Detai_Click(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var ntb = (Note)btn.BindingContext;

        noteData = ntb;
        await Navigation.PushAsync(new MbFavorDetail());
    }
}