using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyNote.Models;
using MyNote.Service;
using MyNote.ViewModels;
using System.Collections.ObjectModel;

namespace MyNote.UI.Mobile.ShellMain;

public partial class MbTodo : ContentPage
{
    public static ObservableCollection<TodoMain> listByUser { get; set; }
    private readonly ITodo _todoService = new TodoVM();
    public static TodoMain todoData;
    private static int usidTd = App.userInfor.UserId;
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    public MbTodo()
	{
		InitializeComponent();
        listByUser = new ObservableCollection<TodoMain>();
        TodoByUser();
        todoView.ItemsSource = listByUser;
        BindingContext = this;
    }
    private async void TodoByUser()
    {
        listByUser.Clear();

        List<TodoMain> list = await _todoService.GetMainByUs(usidTd);
        for (int i = list.Count - 1; i >= 0; i--)
        {
            listByUser.Add(list[i]);
        }

    }

    private async void Add_Click(object sender, EventArgs e)
    {
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        TodoMain todoMain = new TodoMain
        {
            MainName = "Title",
            DateMain = getDate,
            MByUser = usidTd
        };
        bool check = await _todoService.AddTodo(todoMain);
        if (check == true)
        {
           
            listByUser.Clear();
            List<TodoMain> list = await _todoService.GetMainByUs(usidTd);
            for (int i = list.Count - 1; i >= 0; i--)
            {
                listByUser.Add(list[i]);
            }
            todoView.ItemsSource = listByUser;
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

    private async void Search_TC(object sender, TextChangedEventArgs e)
    {
        List<TodoMain> listtodo = new List<TodoMain>();
        listtodo.Clear();
        listtodo = await _todoService.GetMainByUs(usidTd);
        var result = listtodo.Where(a => a.MainName.StartsWith(e.NewTextValue));
        todoView.ItemsSource = result;
    }

    private async void Delete_Click(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var ntb = (TodoMain)btn.BindingContext;
        var result = await DisplayAlert("Delete", $"Do you want delete " + ntb.MainName + "?", "Yes", "No");
        if (result)
        {
            bool check = await _todoService.DeleteTask(ntb.MainId);
            if (check == true)
            {               
                listByUser.Clear();
                List<TodoMain> list = await _todoService.GetMainByUs(usidTd);
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    listByUser.Add(list[i]);
                }
                todoView.ItemsSource = listByUser;
                string text = "Delete Successfully!";
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

    private async void Detail_Click(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var ntb = (TodoMain)btn.BindingContext;

        todoData = ntb;
        await Navigation.PushAsync(new MbTDTask());
    }

    private async void Ref_Click(object sender, EventArgs e)
    {
        listByUser.Clear();

        List<TodoMain> list = await _todoService.GetMainByUs(usidTd);
        for (int i = list.Count - 1; i >= 0; i--)
        {
            listByUser.Add(list[i]);
        }
        todoView.ItemsSource = listByUser;
    }
}