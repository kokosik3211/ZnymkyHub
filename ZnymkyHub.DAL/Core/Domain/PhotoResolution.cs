using System;
using System.Collections.Generic;
using System.Text;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class PhotoResolution
    {
        public int Id { get; set; }

        public string Original { get; set; }
        public string Medium { get; set; }
        public string Small { get; set; }

        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
