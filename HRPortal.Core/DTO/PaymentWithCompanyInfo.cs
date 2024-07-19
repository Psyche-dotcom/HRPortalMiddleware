using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Core.DTO
{
    public class PaymentWithCompanyInfo
    {
        public string Id { get; set; }
        public string Amount { get; set; }
        public string OrderReferenceId { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; }
        public DateTime CreatedPaymentTime { get; set; }
        public DateTime? CompletePaymentTime { get; set; }
        public bool IsActive { get; set; }
        public string PaymentStatus { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
    }

}
