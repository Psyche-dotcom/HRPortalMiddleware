

using HRPortal.Core.DTO;
using HRPortal.Enitities;

namespace HRPortal.Core.Repository.Interface
{
    public interface IPaymentRepo
    {
        Task<Payments> GetPaymentById(string OrderReferenceId);
        Task<bool> AddPayments(Payments payments);
        Task<bool> UpdatePayments(Payments payments);
        Task<PaginatedPaymentInfo> RetrieveAllPaymentAsync(int pageNumber, int perPageSize);
        Task<PaginatedPaymentInfo> RetrieveCompanyAllPaymentAsync(string userid, int pageNumber, int perPageSize);
    }
}