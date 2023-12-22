using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.Models
{
    public class UserDetail
    {
        public int UsDetailId { get; set; }
        public string UsDetailName { get; set; }
        public string UsDetailMail { get; set; }
        public string UsDetailDOB { get; set; }
        public int UsDetailSex { get; set; }
        public int UDByUser { get; set; }
    }
}
