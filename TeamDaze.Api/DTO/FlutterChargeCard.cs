using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamDaze.Api.DTO
{
    //public class 
    //{
    //}

    public class FlutterChargeCard
    {
        public string amount { get; set; }
        public string currency { get; set; }
        public string custid { get; set; }
        public string merchantid { get; set; }
        public string narration { get; set; }
        public string chargetoken { get; set; }
        public string country { get; set; }
        public string cardtype { get; set; }
    }

}