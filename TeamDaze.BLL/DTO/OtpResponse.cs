using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDaze.BLL.DTO
{
   public class OtpResponse
    {
        public string Otp { get; set; }
        public string Bvn { get; set; }
        public int IsUsed { get; set; }
    }
}
