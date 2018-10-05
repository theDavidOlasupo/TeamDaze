using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDaze.BLL.DTO
{
   public class BankPayoutReq
    {
        public string BVN { get; set; }
        public string Amount { get; set; }
        public string TrxnRef { get; set; }
        public string MerchantID { get; set; }
        public string AcctType { get; set; } //savings-1 or current-2
    }
}
