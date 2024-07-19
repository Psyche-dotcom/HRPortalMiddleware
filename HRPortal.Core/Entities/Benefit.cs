namespace HRPortal.Core.Entities
{
    public class Benefit : BaseEntity
    {
        public Staff Staff { get; set; }
        public string StaffId { get; set; }
        public string BenefitType { get; set; }
        public string Provider { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
