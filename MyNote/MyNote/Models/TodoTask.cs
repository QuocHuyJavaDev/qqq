using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.Models
{
    public class TodoTask
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int IsDone { get; set; }
        public int TByMain { get; set; }
    }
}
