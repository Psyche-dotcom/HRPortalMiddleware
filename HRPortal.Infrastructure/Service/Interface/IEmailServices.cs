

using HRPortal.Core.DTO;

namespace HRPortal.Infrastructure.Service.Interface
{
    public interface IEmailServices
    {
        void SendEmail(Message message);
    }
}
