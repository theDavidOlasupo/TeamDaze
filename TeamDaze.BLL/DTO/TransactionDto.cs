using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamDaze.BLL.DTO
{
    public class TransactionDto
    {
        public Int64 ID { get; set; }
        public Int64 CustomerId { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public Int64 MerchantId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
       // public int Status { get; set; }  
    }
}