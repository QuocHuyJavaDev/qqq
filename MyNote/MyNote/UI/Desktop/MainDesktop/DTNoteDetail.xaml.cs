using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyNote.Models;
using MyNote.Service;
using MyNote.ViewModels;
using System.Collections.ObjectModel;

namespace MyNote.UI.Desktop.MainDesktop;

public partial class DTNoteDetail : ContentPage
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
    public ObservableCollection<Note> noteDe { get; set; }
    public DTNoteDetail()
	{
		InitializeComponent();
        noteDe = new ObservableCollection<Note>();
        createModel();
        DetailView.ItemsSource = noteDe;
        BindingContext = this;
        // TextEditor.Text = DTNoteList.noteData.NoteDetail;
    }
    public async void createModel()
    {
        Note note = new Note
        {
            NoteName = DTNoteList.noteData.NoteName,
            NoteDetail = DTNoteList.noteData.NoteDetail,
            DateAddUp = DTNoteList.noteData.DateAddUp,
            IsFavor = DTNoteList.noteData.IsFavor,
            NByNtb = DTNoteList.noteData.NByNtb,
            NByUser = DTNoteList.noteData.NByUser
        };
        noteDe.Add(note);

    }
}