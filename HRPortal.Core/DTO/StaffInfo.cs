using HRPortal.Core.Entities;

namespace HRPortal.Core.DTO
{
    public class StaffInfo
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string? HomeAddress { get; set; }
        public string? HomePhone { get; set; }
        public string? MobilePhone { get; set; }
        public string? Email { get; set; }

        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string Manager { get; set; }
        public string EmploymentType { get; set; }
        public string EmploymentStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WorkLocation { get; set; }
        public string? EmployeePhoto { get; set; }
        public string? Ethnicity { get; set; }
        public string? VeteranStatus { get; set; }
        public string? DisabilityStatus { get; set; }
        public string? Notes { get; set; }
    }
}
