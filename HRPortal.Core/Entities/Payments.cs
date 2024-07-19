using HRPortal.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRPortal.Enitities
{
    public class Payments : BaseEntity
    {
        public string Amount { get; set; }
        public string OrderReferenceId { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; } = "Debit";
        public DateTime CreatedPaymentTime { get; set; } = DateTime.UtcNow;
        public DateTime? CompletePaymentTime { get; set; }
        public bool IsActive { get; set; } = true;
        public int SubscriptionValidity { get; set; }
        public string PaymentStatus { get; set; } = "CREATED";

        [ForeignKey(nameof(ApplicationCompany))]
        public string CompanyId { get; set; }

        public ApplicationCompany Company { get; set; }
        public string PaymentChannel { get; set; }
    }
}