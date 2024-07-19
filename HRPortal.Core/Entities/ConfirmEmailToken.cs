namespace HRPortal.Core.Entities
{
    public class ConfirmEmailToken : BaseEntity
    {
        public int Token { get; set; }
        public string UserId { get; set; }
        public ApplicationCompany User { get; set; }
    }
}
