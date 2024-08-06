namespace HRPortal.Core.DTO
{
    public class UpdateStaffDto
    {
        public string Id { get; set; }

        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }


        public string? HomeAddress { get; set; }
        public string? HomePhone { get; set; }
        public string? MobilePhone { get; set; }
        public string? Email { get; set; }
    }
}
