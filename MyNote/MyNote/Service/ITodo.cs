using MyNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.Service
{
    public interface ITodo
    {
        Task<List<TodoMain>> GetMainByUs(int userid);
        Task<bool> AddTodo(TodoMain tdMain);
        Task<bool> NameChange(int mainid, TodoMain tdmain);
        Task<bool> DeleteTask(int mainid);
    }
}
