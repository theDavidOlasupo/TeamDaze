using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamDaze.Api.DTO
{
    public class TransactionDto
    {
        public string CustomerId { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public string MerchantId { get; set; }
        public string Amount { get; set; }
        public DateTime CreatedOn { get; set; }
       // public int Status { get; set; }  
    }
}