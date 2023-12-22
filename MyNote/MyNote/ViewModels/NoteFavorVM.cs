using MyNote.Models;
using MyNote.UI.Mobile.ShellMain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.ViewModels
{
    public class NoteFavorVM
    {
        public Note noteFavor;
        public Note NoteFavorView
        {
            get => noteFavor;
        }

        public NoteFavorVM()
        {
            noteFavor = MbFavorDetail.createModel();
        }
    }
}
