using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MyNote.Models;
using MyNote.Service;
using MyNote.ViewModels;
using System.Collections.ObjectModel;

namespace MyNote.UI.Desktop.MainDesktop;

public partial class DTTask : ContentPage
{
    public ObservableCollection<TodoTask> listTask { get; set; }
    private readonly ITDTask _taskService = new TDTaskVM();
    private readonly ITodo _todoService = new TodoVM();
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    ToastDuration duration = ToastDuration.Short;
    double fontSize = 14;
    private static int mainid = DTTodo.todoData.MainId;
    public DTTask()
	{
		InitializeComponent();
        TodoTit.Text = DTTodo.todoData.MainName;
        listTask = new ObservableCollection<TodoTask>();
        TaskLoad();
        todoView.ItemsSource = listTask;
        BindingContext = this;
    }
    private async void TaskLoad()
    {
        listTask.Clear();
        List<TodoTask> list = await _taskService.GetTask(mainid);
        for (int i = 0; i < list.Count; i++)
        {
            listTask.Add(list[i]);
        }

    }
    private async void Add_Click(object sender, EventArgs e)
    {
        TodoTask taskModel = new TodoTask
        {
            TaskName = "New task",
            IsDone = 0,
            TByMain = mainid
        };
        bool check = await _taskService.AddTask(taskModel);
        if (check == true)
        {
            listTask.Clear();
            List<TodoTask> list = await _taskService.GetTask(mainid);
            for (int i = 0; i < list.Count; i++)
            {
                listTask.Add(list[i]);
            }
            todoView.ItemsSource = listTask;
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
        List<TodoTask> listtask = new List<TodoTask>();
        listtask.Clear();
        listtask = await _taskService.GetTask(mainid);
        var result = listtask.Where(a => a.TaskName.StartsWith(e.NewTextValue));
        todoView.ItemsSource = result;
    }

    private async void EditName_Complete(object sender, EventArgs e)
    {
        string getDate = DateTime.Now.ToString("dd/MM/yyyy");
        TodoMain tdMainObj = new TodoMain
        {
            MainName = TodoTit.Text,
            DateMain = getDate,
            MByUser = App.userInfor.UserId
        };
        bool check = await _todoService.NameChange(mainid, tdMainObj);
        if (check == true)
        {
            string text = "Update Successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
            DTTodo.listByUser.Clear();
            List<TodoMain> list = await _todoService.GetMainByUs(App.userInfor.UserId);
            for (int i = list.Count - 1; i >= 0; i--)
            {
                DTTodo.listByUser.Add(list[i]);
            }
        }
        else
        {
            string text = "Update Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void unCheck_click(object sender, EventArgs e)
    {
        var btn = (ImageButton)sender;
        var ntb = (TodoTask)btn.BindingContext;
        TodoTask taskMode = new TodoTask
        {
            TaskName = "f",
            IsDone = 1,
            TByMain = mainid
        };
        bool check = await _taskService.DoneChange(ntb.TaskId, taskMode);
        if (check == true)
        {
            listTask.Clear();
            List<TodoTask> list = await _taskService.GetTask(mainid);
            for (int i = 0; i < list.Count; i++)
            {
                listTask.Add(list[i]);
            }
            todoView.ItemsSource = listTask;
            string text = "Update Successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = "Update Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void Check_click(object sender, EventArgs e)
    {
        var btn = (ImageButton)sender;
        var ntb = (TodoTask)btn.BindingContext;
        TodoTask taskMode = new TodoTask
        {
            TaskName = "f",
            IsDone = 0,
            TByMain = mainid
        };
        bool check = await _taskService.DoneChange(ntb.TaskId, taskMode);
        if (check == true)
        {
            listTask.Clear();
            List<TodoTask> list = await _taskService.GetTask(mainid);
            for (int i = 0; i < list.Count; i++)
            {
                listTask.Add(list[i]);
            }
            todoView.ItemsSource = listTask;
            string text = "Update Successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = "Update Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void Task_Compete(object sender, EventArgs e)
    {
        var btn = (Entry)sender;
        var ntb = (TodoTask)btn.BindingContext;
        TodoTask taskModel = new TodoTask
        {
            TaskName = btn.Text,
            IsDone = 0,
            TByMain = 0
        };
        bool check = await _taskService.TaskNameChange(ntb.TaskId, taskModel);
        if (check == true)
        {
            listTask.Clear();
            List<TodoTask> list = await _taskService.GetTask(mainid);
            for (int i = 0; i < list.Count; i++)
            {
                listTask.Add(list[i]);
            }
            todoView.ItemsSource = listTask;
            string text = "Update Successfully!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            string text = "Update Failed!";
            var toast = Toast.Make(text, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private async void Del_Click(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var ntb = (TodoTask)btn.BindingContext;
        bool check = await _taskService.DeleteTask(ntb.TaskId);
        if (check == true)
        {
            listTask.Clear();
            List<TodoTask> list = await _taskService.GetTask(mainid);
            for (int i = 0; i < list.Count; i++)
            {
                listTask.Add(list[i]);
            }
            todoView.ItemsSource = listTask;
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