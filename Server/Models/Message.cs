using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    /// <summary>
    /// Data transfer object to send messages via server
    /// </summary>
    public class Message
    {
        public Client? Client { get; set; }
        public string? Text { get; set; }
        public DateTime Time { get; set; }
    }
}
