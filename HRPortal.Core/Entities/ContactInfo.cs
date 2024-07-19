namespace HRPortal.Core.Entities
{
    public class ContactInfo : BaseEntity
    {
        public string? HomeAddress { get; set; }
        public string? HomePhone { get; set; }
        public string? MobilePhone { get; set; }
        public string? Email { get; set; }
        public Staff Staff { get; set; }
        public string StaffId { get; set; }
    }
}
