using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public enum RecordAccess
    {
        Digital_Download = 1,
        Streaming = 2
    };
    public class Record
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public RecordAccess[] Usages { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
