namespace NoteAPI.Model
{
    public class Schedule
    {
        public int EventId { get; set; }
        public string EventStart { get; set; }
        public string EventEnd { get; set; }
        public string EventName { get; set; }
        public int EByUser { get; set; }
    }
}
