using System;
using System.Collections.Generic;
using System.Text;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class PhotoResolution
    {
        public int Id { get; set; }

        public byte[] Original { get; set; }
        public byte[] Medium { get; set; }
        public byte[] Small { get; set; }

        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
