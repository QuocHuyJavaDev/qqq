using MyNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.Service
{
    public interface ITDTask
    {
        Task<List<TodoTask>> GetTask(int mainid);
        Task<bool> DoneChange(int taskid, TodoTask tdtask);
        Task<bool> TaskNameChange(int taskid, TodoTask tdtask);
        Task<bool> AddTask(TodoTask tdtask);
        Task<bool> DeleteTask(int taskId);
    }
}
