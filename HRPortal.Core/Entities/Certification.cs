namespace HRPortal.Core.Entities
{
    public class Certification : BaseEntity
    {
        public Staff Staff { get; set; }
        public string StaffId { get; set; }
        public string CertificationName { get; set; }
        public string IssuingOrganization { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}

