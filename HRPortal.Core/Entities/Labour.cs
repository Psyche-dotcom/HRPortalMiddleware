namespace HRPortal.Core.Entities
{
    public class Labour : BaseEntity
    {
        public ApplicationCompany Company { get; set; }
        public string CompanyId { get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }
        public string LCAT { get; set; }
        public string WorkSite { get; set; }
        public string ChargeCode { get; set; }
        public string ReminderDays { get; set; }
        public string Reminder { get; set; }
        public IEnumerable<Staff> Staffs { get; set; }

    }
}
