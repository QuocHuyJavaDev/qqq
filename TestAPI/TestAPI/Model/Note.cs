namespace NoteAPI.Model
{
    public class Note
    {
        public int NoteId { get; set; }
        public string NoteName { get; set; }
        public string NoteDetail { get; set; }
        public string DateAddUp { get; set; }
        public int IsFavor { get; set;}
        public int NByNtb { get; set; }
        public int NByUser { get; set; }
    }
}
