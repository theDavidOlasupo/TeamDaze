using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamDaze.BLL.DTO
{
    public class CustomerCreation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BVN { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PanicFinger { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public decimal MaxAmount { get; set; }
        public string EnrollmentType { get; set; }
        public string CardType { get; set; }
        public string CardToken { get; set; }
    }
}
