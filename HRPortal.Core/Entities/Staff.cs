namespace HRPortal.Core.Entities
{
    public class Staff : BaseEntity
    {
        public string CompanyId { get; set; }
        public ApplicationCompany Company { get; set; }
        public string? LabourId { get; set; }
        public Labour Labour { get; set; }
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
      
        public ContactInfo ContactInformation { get; set; }


        // Compensation and Benefits
        public Compensation Compensation { get; set; }
        public List<Benefit> Benefits { get; set; }
        public TaxInformation TaxInformation { get; set; }

        // Emergency Contact Information
        public EmergencyContact EmergencyContact { get; set; }

        // Education and Experience
        public List<Education> EducationHistory { get; set; }
        public List<Certification> Certifications { get; set; }
        public List<EmploymentHistory> PreviousEmployments { get; set; }

        // Performance and Training
        public List<PerformanceReview> PerformanceReviews { get; set; }
        public List<TrainingRecord> TrainingRecords { get; set; }



        public List<CompanyProperty> CompanyProperties { get; set; }

        

    }
}
