using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neco.DataTransferObjects
{
    public class MessageDto : BaseDto
    {
        public DateTime Timestamp { get; set; }

        public string AuthorName { get; set; }

        public string Contnet { get; set; } 
    }
}
