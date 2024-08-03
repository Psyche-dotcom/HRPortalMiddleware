using HRPortal.Core.DTO;
using HRPortal.Core.Entities;

namespace HRPortal.Infrastructure.Service.Interface
{
    public interface ILabourService
    {
        Task<ResponseDto<string>> AddLabour(AddLabourDto labourinfo, string companyid);
        Task<ResponseDto<string>> DeleteLabour(string staffid);
        Task<ResponseDto<string>> UpdateLabourinfo(UpdateLabourDto labourinfo);
        Task<ResponseDto<IEnumerable<Labour>>> GetAllLabour(string companyid);
        Task<ResponseDto<Labour>> GetSingleLabour(string companyid, string labourid);
    }
}
