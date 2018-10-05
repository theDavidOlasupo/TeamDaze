using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamDaze.BLL.DTO
{

    public class MatchWithPhoneNumrReq
    {
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string ISOTemplate { get; set; }
    }

}