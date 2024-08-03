using HRPortal.Core.DTO;

namespace HRPortal.Infrastructure.Service.Interface
{
    public interface IStaffService
    {
        Task<ResponseDto<string>> AddStaff(AddStaffDto staffinfo, string companyid);
        Task<ResponseDto<PaginatedGenericDto<IEnumerable<StaffInfo>>>> GetAllStaff(int pageNumber, int perPageSize, string companyid);
        Task<ResponseDto<string>> DeleteStaff(string staffid);
        Task<ResponseDto<StaffInfo>> GetSingleStaff(string staffid, string companyid);
        Task<ResponseDto<string>> UpdateStaffinfo(UpdateStaffDto staffinfo);
        Task<ResponseDto<string>> AssignStafftoLabour(string staffId, string labourid);
    }
}
