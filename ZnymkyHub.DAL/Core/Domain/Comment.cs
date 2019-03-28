using System;
using System.Collections.Generic;
using System.Text;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class Comment
    {
        public int Id { get; set; }

        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime DateTime { get; set; }

        public string Text { get; set; }
    }
}
