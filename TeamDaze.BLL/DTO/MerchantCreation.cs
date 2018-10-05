using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamDaze.BLL.DTO
{
    public class MerchantCreation
    {
        public string Name { get; set; }
        public string SettlementAccount { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
    }
}
