using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamDaze.Api.DTO
{
    //public class 
    //{
    //}

    public class FlutterCardResponse 
    {
        public Data data { get; set; }
        public string status { get; set; }
    }

    public class Data
    {
        public string responsecode { get; set; }
        public string responsemessage { get; set; }
        public object otptransactionidentifier { get; set; }
        public string transactionreference { get; set; }
        public object responsetoken { get; set; }
    }

}