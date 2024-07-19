using HRPortal.Enitities;
using Microsoft.AspNetCore.Identity;


namespace HRPortal.Core.Entities
{
    public class ApplicationCompany : IdentityUser
    {
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public bool isSuspended { get; set; } = false;
        public bool isSubscribed { get; set; } = false;
        public CompanySubscription Subscription { get; set; }
        public ConfirmEmailToken ConfirmEmailToken { get; set; }
        public ForgetPasswordToken ForgetPasswordToken { get; set; }
        public IEnumerable<Staff> Staffs { get; set; }
        public IEnumerable<Payments> Payments { get; set; }
    }
}
