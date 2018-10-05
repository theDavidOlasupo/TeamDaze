using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDaze.BLL.DTO
{
    //public  class FlutterCardResponse
    //  {
    //      public int MyProperty { get; set; }
    //      public int MyProperty { get; set; }
    //  }

    public class FlutterCardResponse
    {
        public Data data { get; set; }
        public string status { get; set; }
    }

    public class Data
    {
        public string responsecode { get; set; }
        public string authorizeId { get; set; }
        public string responsemessage { get; set; }
        public object otptransactionidentifier { get; set; }
        public string transactionreference { get; set; }
        public object responsehtml { get; set; }
        public object responsetoken { get; set; }
    }

}
