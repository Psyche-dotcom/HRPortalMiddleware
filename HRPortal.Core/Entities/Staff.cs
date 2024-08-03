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
        public string MaritalStatus { get; set; }
        public string SocialSecurityNumber { get; set; }
        public ContactInfo ContactInformation { get; set; }

        // Employment Information
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string Manager { get; set; }
        public string EmploymentType { get; set; }
        public string EmploymentStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WorkLocation { get; set; }
        public WorkSchedule WorkSchedule { get; set; }

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



        // Additional Information
        public string? EmployeePhoto { get; set; }
        public List<CompanyProperty> CompanyProperties { get; set; }

        // Diversity and Inclusion (Optional)
        public string? Ethnicity { get; set; }
        public string? VeteranStatus { get; set; }
        public string? DisabilityStatus { get; set; }
        // Notes
        public string? Notes { get; set; }

    }
}
