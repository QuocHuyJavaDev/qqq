using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyNote.Models;
using MyNote.Service;
using MyNote.UI.Desktop.MainDesktop;
using MyNote.ViewModels;

namespace MyNote.UI.Desktop.DTPopup;

public partial class DTEditEvent : ContentPage
{
    public readonly ISchedule _scheService = new ScheduleVM();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public DTEditEvent()
	{
		InitializeComponent();
        txtEventName.Text = DTSchedule.saData.Location;
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
        int scheid = int.Parse(DTSchedule.saData.Notes);
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
            await Navigation.PushAsync(new DTSchedule());
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