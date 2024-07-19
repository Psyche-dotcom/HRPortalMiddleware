namespace HRPortal.Core.Entities
{
    public class CompanySubscription : BaseEntity
    {
        public ApplicationCompany Company { get; set; }
        public string CompanyId { get; set; }
        public DateTime SubscrptionStart { get; set; }
        public DateTime SubscrptionEnd { get; set; }
    }
}
