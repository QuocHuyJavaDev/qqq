using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.Models
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
