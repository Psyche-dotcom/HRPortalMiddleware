namespace HRPortal.Core.DTO
{
    public class CompanyInfo
    {
        public string Id { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public bool isSuspended { get; set; }
        public bool isSubscribed { get; set; }
        public DateTime SubscrptionStart { get; set; }
        public DateTime SubscrptionEnd { get; set; }
    }
}
