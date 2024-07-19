

using HRPortal.Core.Entities;

namespace HRPortal.Infrastructure.Service.Interface
{
    public interface IGenerateJwt
    {
        Task<string> GenerateToken(ApplicationCompany user);
    }
}
