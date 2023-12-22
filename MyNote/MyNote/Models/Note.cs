using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public string NoteName { get; set; }
        public string NoteDetail { get; set; }
        public string DateAddUp { get; set; }
        public int IsFavor { get; set; }
        public int NByNtb { get; set; }
        public int NByUser { get; set; }
    }
}
