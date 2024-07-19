namespace HRPortal.Core.DTO
{
    public class LoginResultDto
    {
        public string Jwt { get; set; }
        public IList<string> UserRole { get; set; }
    }
}
