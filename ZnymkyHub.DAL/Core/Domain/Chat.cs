using System;
using System.Collections.Generic;
using System.Text;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class Chat
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public User Sender { get; set; }

        public int ReceiverId { get; set; }
        public User Receiver { get; set; }

        public DateTime DateTime { get; set; }

        public string Text { get; set; }
    }
}
