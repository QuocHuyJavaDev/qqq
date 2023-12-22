using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.Models
{
    public class Notebook
    {
        public int NotebookId { get; set; }
        public string NotebookName { get; set; }
        public string DateCreate { get; set; }
        public int ByUser { get; set; }

    }
}
