using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyNote.Models;
using MyNote.Service;
using MyNote.UI.Mobile.ShellMain;
using MyNote.ViewModels;
using System.Runtime.Intrinsics.X86;

namespace MyNote.UI.Mobile.PopupView;

public partial class MbEditEvent : ContentPage
{
    public readonly ISchedule _scheService = new ScheduleVM();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public MbEditEvent()
    {
		InitializeComponent();
        txtEventName.Text = MbSchedule.saData.Location;
    }

    private async void back_click(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void Save_Click(object sender, EventArgs e)
    {
        DateTime dateStart = dayStart.Date;
        DateTime dateEnd = dayEnd.Date;
        var timestart = timeStarr.Time;
        var timeend = timeEnd.Time;
        string[] timestartArr = timestart.ToString().Split(':');
        string[] timeendArr = timeend.ToString().Split(':');
        string startString = dateStart.ToString("yyyy M d") + " " + timestartArr[0] + " " + timestartArr[1] + " 0";
        string endString = dateEnd.ToString("yyyy M d") + " " + timeendArr[0] + " " + timeendArr[1] + " 0";
        //
        string eventName = txtEventName.Text;
        int userid = App.userInfor.UserId;
        int scheid = int.Parse(MbSchedule.saData.Notes);
        Schedule schedule = new Schedule
        {
            EventStart = startString,
            EventEnd = endString,
            EventName = eventName,
            EByUser = userid
        };
        bool check = await _scheService.EditSche(scheid, schedule);
        if (check == true)
        {
            await Navigation.PushAsync(new MbSchedule());
            string text = "Change Successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = "Change Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }
}