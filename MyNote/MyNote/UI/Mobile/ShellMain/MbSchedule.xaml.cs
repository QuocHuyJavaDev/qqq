using MyNote.Models;
using MyNote.Service;
using MyNote.ViewModels;
using Syncfusion.Maui.Scheduler;
using MyNote.UI.Mobile.PopupView;
using System.Collections.ObjectModel;
using Plugin.LocalNotification;

namespace MyNote.UI.Mobile.ShellMain;

public partial class MbSchedule : ContentPage
{
    public ObservableCollection<SchedulerAppointment> ScheduleEvents { get; set; }
    private readonly ISchedule _scheService = new ScheduleVM();
    public MbSchedule()
	{
		InitializeComponent();
        this.ScheduleEvents = new ObservableCollection<SchedulerAppointment>();
        EventList();
        scheData.AppointmentsSource = ScheduleEvents;
        Setup();
    }
    private async void EventList()
    {
        ScheduleEvents.Clear();
        int userid = App.userInfor.UserId;
        List<Schedule> list = await _scheService.GetScheList(userid);
        for (int i = list.Count - 1; i >= 0; i--)
        {
            string[] getStart = list[i].EventStart.Split(' ');
            string[] getEnd = list[i].EventEnd.Split(' ');
            string startFormat = string.Format($"{getStart[3]}:{getStart[4]} {getStart[2]}/{getStart[1]}/{getStart[0]}");
            string endFormat = string.Format($"{getEnd[3]}:{getEnd[4]} {getEnd[2]}/{getEnd[1]}/{getEnd[0]}");
            ScheduleEvents.Add(new SchedulerAppointment
            {
                StartTime = new DateTime(int.Parse(getStart[0]), int.Parse(getStart[1]), int.Parse(getStart[2]),
                int.Parse(getStart[3]), int.Parse(getStart[4]), int.Parse(getStart[5])),
                EndTime = new DateTime(int.Parse(getEnd[0]), int.Parse(getEnd[1]), int.Parse(getEnd[2]),
                int.Parse(getEnd[3]), int.Parse(getEnd[4]), int.Parse(getEnd[5])),
                Subject = list[i].EventName,
                Notes = list[i].EventId.ToString(),
                Location = list[i].EventName,
                Background = Color.Parse("#C664BE")
            });
        }
    }
    private async void Setup()
    {
        Device.StartTimer(new TimeSpan(0, 0, 1), () =>
        {
            foreach (var evt in ScheduleEvents)
            {
                var timespan = evt.StartTime - DateTime.Now;
                //evt.Timespan = timespan;
                //var a = new TimeSpan(0, 0, 0, 0);
                if (timespan.Days == 0 && timespan.Hours == 0 && timespan.Minutes == 5 && timespan.Seconds == 0)
                {
                    App.Current.MainPage.DisplayAlert("Alert", "Only 5 minutes left to " + evt.Subject, "Okay");
                    var request = new NotificationRequest
                    {
                        NotificationId = 1234,
                        Title = evt.Subject,
                        Subtitle = "Hello",
                        Description = "Only 5 minutes left to " + evt.Subject,
                        BadgeNumber = 42,
                        Schedule = new NotificationRequestSchedule
                        {
                            NotifyTime = DateTime.Now.AddSeconds(5),
                            NotifyRepeatInterval = TimeSpan.FromDays(1),
                        }
                    };
                    LocalNotificationCenter.Current.Show(request);
                }
                if (timespan.Days == 0 && timespan.Hours == 0 && timespan.Minutes == 0 && timespan.Seconds == 0)
                {
                    App.Current.MainPage.DisplayAlert("Time up", "It's time to " + evt.Subject, "Okay");
                    var request = new NotificationRequest
                    {
                        NotificationId = 1235,
                        Title = evt.Subject,
                        Subtitle = "Time up",
                        Description = "It's time to " + evt.Subject,
                        BadgeNumber = 43,
                        Schedule = new NotificationRequestSchedule
                        {
                            NotifyTime = DateTime.Now.AddSeconds(5),
                            NotifyRepeatInterval = TimeSpan.FromDays(1),
                        }
                    };
                    LocalNotificationCenter.Current.Show(request);
                }
            }
            return true;
        });
    }
    private async void AddNtb_Click(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MbAddEvent());
    }

    public static SchedulerAppointment saData;
    private async void Tap_Events(object sender, SchedulerTappedEventArgs e)
    {
        if (e.Appointments != null)
        {
            var appointments = e.Appointments;
            SchedulerAppointment[] arr = new SchedulerAppointment[appointments.Count];
            appointments.CopyTo(arr, 0);
            saData = arr[0];
            await Navigation.PushAsync(new MbEditEvent());
        }
        else
        {
            await DisplayAlert("Event", $"No event", "Ok");
        }
    }
}