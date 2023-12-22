using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyNote.Models;
using MyNote.Service;
using MyNote.ViewModels;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

namespace MyNote.UI.Mobile.ShellMain;

public partial class MbNDetail : ContentPage
{
    private INote _noteSer = new NoteVM();
    private readonly INote _noteService = new NoteVM();
    private bool isChange = false;
    private string oldTitle;
    private string newTitle;
    private string oldDetail;
    private string newDetail;
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public MbNDetail()
	{
		InitializeComponent();
        this.BindingContext = new NoteDetailVM();
        oldTitle = entryTit.Text;
        oldDetail = TextEditor.Text;
        int isFavor = MBNoteList.noteData.IsFavor;
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
            NoteName = MBNoteList.noteData.NoteName,
            NoteDetail = MBNoteList.noteData.NoteDetail,
            DateAddUp = MBNoteList.noteData.DateAddUp,
            IsFavor = MBNoteList.noteData.IsFavor,
            NByNtb = MBNoteList.noteData.NByNtb,
            NByUser = MBNoteList.noteData.NByUser
        };
        return note;
    }

    private void sortNtbList()
    {
        for (int i = 0; i < MBNoteList.noteByNtb.Count - 1; i++)
        {
            for (int j = i; j < MBNoteList.noteByNtb.Count; j++)
            {
                if (MBNoteList.noteByNtb[i].NoteId < MBNoteList.noteByNtb[j].NoteId)
                {
                    Note temp = MBNoteList.noteByNtb[i];
                    MBNoteList.noteByNtb[i] = MBNoteList.noteByNtb[j];
                    MBNoteList.noteByNtb[j] = temp;
                }
            }
        }
    }
    private async void back_click(object sender, EventArgs e)
    {
        MBNoteList.noteByNtb.Clear();
        int ntbid = MbNotebook.ntb2.NotebookId;
        List<Note> list = await _noteService.GetByNtbId(ntbid);
        for (int i = list.Count - 1; i >= 0; i--)
        {
            MBNoteList.noteByNtb.Add(list[i]);
        }
        sortNtbList();
        await Navigation.PopAsync();
    }

    private async void Save_Click(object sender, EventArgs e)
    {
        string nName = entryTit.Text;
        string nDetail = TextEditor.Text;
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        int userid = App.userInfor.UserId;
        int ntbId = MbNotebook.ntb2.NotebookId;
        int noteid = MBNoteList.noteData.NoteId;
        Note note = new Note
        {
            NoteName = nName,
            NoteDetail = nDetail,
            DateAddUp = getDate,
            IsFavor = 0,
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
        int noteid = MBNoteList.noteData.NoteId;
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
            MbHome.favorNote.Clear();
            int userid = App.userInfor.UserId;
            List<Note> list = await _noteService.GetFavpr(1, userid);
            for (int i = list.Count - 1; i >= 0; i--)
            {
                MbHome.favorNote.Add(list[i]);
            }
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
        int noteid = MBNoteList.noteData.NoteId;
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
            MbHome.favorNote.Clear();
            int userid = App.userInfor.UserId;
            List<Note> list = await _noteService.GetFavpr(1, userid);
            for (int i = list.Count - 1; i >= 0; i--)
            {
                MbHome.favorNote.Add(list[i]);
            }
        }
        else
        {
            string text = "Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private void Export_Click(object sender, EventArgs e)
    {
        //PdfDocument document = new PdfDocument();
        //PdfPage page = document.Pages.Add();
        //PdfGraphics graphics = page.Graphics;
        //PdfFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14, PdfFontStyle.Regular);

        //string htmlText = $"<font face='TimesRoman' size='18'><i><b><u>{MBNoteList.noteData.NoteName}</u></b></i></font>" +
        //    $"<br></br><br></br>{MBNoteList.noteData.NoteDetail}";

        //PdfHTMLTextElement richTextElement = new PdfHTMLTextElement(htmlText, font, PdfBrushes.Black);

        //PdfLayoutFormat format = new PdfLayoutFormat();
        //format.Layout = PdfLayoutType.Paginate;
        //format.Break = PdfLayoutBreakType.FitPage;

        //richTextElement.Draw(page, new RectangleF(0, 20, page.GetClientSize().Width, page.GetClientSize().Height), format);
        //using MemoryStream ms = new();
        //document.Save(ms);
        //ms.Position = 0;
        //SaveService saveService = new();
        //saveService.SaveAndView($"{MBNoteList.noteData.NoteName}.pdf", "application/pdf", ms);
    }

    private async void Delete_Click(object sender, EventArgs e)
    {
        var result = await DisplayAlert("Delete", $"Do you want delete note " + MBNoteList.noteData.NoteName + "?", "Yes", "No");
        if (result)
        {
            bool check = await _noteSer.DeleteNote(MBNoteList.noteData.NoteId);
            if (check == true)
            {
                await Navigation.PopAsync();
                MBNoteList.noteByNtb.Clear();
                int ntbid = MbNotebook.ntb2.NotebookId;
                List<Note> list = await _noteService.GetByNtbId(ntbid);
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    MBNoteList.noteByNtb.Add(list[i]);
                }
                sortNtbList();
                string text = "Delete Succesfully!";
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show(cancellationTokenSource.Token);

            }
            else
            {
                string text = "Delete Failed!";
                var toast = Toast.Make(text, duration, fontSize);
                await toast.Show(cancellationTokenSource.Token);
            }
        }
    }
}