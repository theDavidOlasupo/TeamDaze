using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamDaze.Api.DTO
{
    //public class 
    //{
    //}

    public class TokenizeCard
    {
        public Data2 data { get; set; }
        public string status { get; set; }
    }

    public class Data2
    {
        public string responsecode { get; set; }
        public string responsemessage { get; set; }
        public object otptransactionidentifier { get; set; }
        public string transactionreference { get; set; }
        public string responsetoken { get; set; }
    }

}