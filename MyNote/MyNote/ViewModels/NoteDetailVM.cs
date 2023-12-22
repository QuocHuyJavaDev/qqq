using MyNote.Models;
using MyNote.UI.Mobile.ShellMain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.ViewModels
{
    public class NoteDetailVM
    {
        public Note noteData;
        public Note NoteView
        {
            get => noteData;
        }

        public NoteDetailVM()
        {
            noteData = MbNDetail.createModel();
        }
        //
        

    }
}
