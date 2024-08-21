using HRPortal.Core.Entities;

namespace HRPortal.Core.DTO
{
    public class StaffInfo
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
      
        public string FullName { get; set; }
    
        public string? MobilePhone { get; set; }
        public string? Email { get; set; }
        public string? LabourName { get; set; }
        public string? LabourCustomer { get; set; }
        public string? LabourLCAT { get; set; }
        public string? LabourWorkSite { get; set; }
        public string? LabourChargeCode { get; set; }


    }
}
