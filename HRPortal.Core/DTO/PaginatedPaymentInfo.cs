namespace HRPortal.Core.DTO
{
    public class PaginatedPaymentInfo
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<PaymentWithCompanyInfo> Payments { get; set; }
    }
}
