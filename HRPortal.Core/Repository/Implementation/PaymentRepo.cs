
using HRPortal.Core.Context;
using HRPortal.Core.DTO;
using HRPortal.Core.Repository.Interface;
using HRPortal.Enitities;
using Microsoft.EntityFrameworkCore;
namespace HRPortal.Repository.Implementation
{
    public class PaymentRepo : IPaymentRepo
    {
        private HRPortalContext _context;

        public PaymentRepo(HRPortalContext context)
        {
            _context = context;
        }

        public async Task<Payments> GetPaymentById(string OrderReferenceId)
        {
            return await _context.Payments.FirstOrDefaultAsync(x => x.OrderReferenceId == OrderReferenceId);
        }
        public async Task<PaginatedPaymentInfo> RetrieveAllPaymentAsync(int pageNumber, int perPageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            perPageSize = perPageSize < 1 ? 5 : perPageSize;
            var payment = _context.Payments.Include(u => u.Company).Select(p => new PaymentWithCompanyInfo
            {
                Id = p.Id,
                Amount = p.Amount,
                OrderReferenceId = p.OrderReferenceId,
                Description = p.Description,
                PaymentType = p.PaymentType,
                CreatedPaymentTime = p.CreatedPaymentTime,
                CompletePaymentTime = p.CompletePaymentTime,
                IsActive = p.IsActive,
                CompanyId = p.CompanyId,
                PaymentStatus = p.PaymentStatus,
                CompanyName = p.Company.CompanyName,
               CompanyEmail = p.Company.Email
               
            });
            var paginatedPayment = await payment
                .Skip((pageNumber - 1) * perPageSize)
                .Take(perPageSize)
                .ToListAsync();
            var totalCount = await payment.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);
            var result = new PaginatedPaymentInfo
            {
                CurrentPage = pageNumber,
                PageSize = perPageSize,
                TotalPages = totalPages,
                Payments = paginatedPayment,
            };
            return result;

            
        }
        public async Task<PaginatedPaymentInfo> RetrieveCompanyAllPaymentAsync(string companyid, int pageNumber, int perPageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            perPageSize = perPageSize < 1 ? 5 : perPageSize;
            var paymentsWithCompanyInfo = _context.Payments
                .Include(p => p.Company)
                .Where(p => p.CompanyId == companyid)
                .Select(p => new PaymentWithCompanyInfo
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    OrderReferenceId = p.OrderReferenceId,
                    Description = p.Description,
                    PaymentType = p.PaymentType,
                    CreatedPaymentTime = p.CreatedPaymentTime,
                    CompletePaymentTime = p.CompletePaymentTime,
                    IsActive = p.IsActive,
                    CompanyId = p.CompanyId,
                    PaymentStatus = p.PaymentStatus,
                    CompanyName = p.Company.CompanyName,
                    CompanyEmail = p.Company.Email
                });
            var paginatedPayment = await paymentsWithCompanyInfo
                .Skip((pageNumber - 1) * perPageSize)
                .Take(perPageSize)
                .ToListAsync();
            var totalCount = await paymentsWithCompanyInfo.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);
            var result = new PaginatedPaymentInfo
            {
                CurrentPage = pageNumber,
                PageSize = perPageSize,
                TotalPages = totalPages,
                Payments = paginatedPayment,
            };
            return result;

            
        }


        public async Task<bool> AddPayments(Payments payments)
        {
            await _context.Payments.AddAsync(payments);
            if ( await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdatePayments(Payments payments)
        {
             _context.Payments.Update(payments);
            if ( await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}