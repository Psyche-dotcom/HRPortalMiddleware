using System.ComponentModel.DataAnnotations;

namespace HRPortal.Core.DTO
{
    public class SignUp
    {

        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Location { get; set; }
      

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       
    }
}
