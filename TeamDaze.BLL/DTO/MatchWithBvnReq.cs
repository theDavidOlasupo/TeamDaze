using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamDaze.BLL.DTO
{
    public class MatchWithBvnReq
    {
        public string BVN { get; set; }
        public string Position { get; set; }
        public string ISOTemplate { get; set; }
    }
}