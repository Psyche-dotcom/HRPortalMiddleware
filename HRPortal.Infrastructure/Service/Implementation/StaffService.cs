using AutoMapper;
using HRPortal.Core.DTO;
using HRPortal.Core.Entities;
using HRPortal.Core.Repository.Interface;
using HRPortal.Infrastructure.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HRPortal.Infrastructure.Service.Implementation
{
    public class StaffService : IStaffService
    {
        private readonly IHRPortalRepository<Staff> _staffRepo;
        private readonly IHRPortalRepository<ContactInfo> _contactRepo;
        private readonly ILogger<StaffService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public StaffService(IHRPortalRepository<Staff> staffRepo,
            ILogger<StaffService> logger,
            IConfiguration configuration, 
            IMapper mapper, 
            IHRPortalRepository<ContactInfo> contactRepo)
        {
            _staffRepo = staffRepo;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _contactRepo = contactRepo;
        }

        public async Task<ResponseDto<string>> AddStaff(AddStaffDto staffinfo, string companyid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var mapStaff = _mapper.Map<Staff>(staffinfo);
                mapStaff.CompanyId = companyid;
                var staff = await _staffRepo.Add(mapStaff);
                var mapContact = _mapper.Map<ContactInfo>(staffinfo);
                mapContact.StaffId = staff.Id;
                await _contactRepo.Add(mapContact);
                await _staffRepo.SaveChanges();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "Staff successfully added";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in adding staff for company" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<string>> DeleteStaff(string staffid)
        {
            var response = new ResponseDto<string>();
            try
            {

               var findStaff =  await _staffRepo.GetByIdAsync(staffid);
                if (findStaff == null)
                {
                    response.ErrorMessages = new List<string>() { "Staff not available for deletion" };
                    response.DisplayMessage ="Error";
                    response.StatusCode = 404;
                    return response;
                }
                 _staffRepo.Delete(findStaff);
                await _staffRepo.SaveChanges();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "Staff deleted successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in deleting staff for company" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<string>> UpdateStaffinfo(UpdateStaffDto staffinfo)
        {
            var response = new ResponseDto<string>();
            try
            {

                var findStaff = await _staffRepo.GetByIdAsync(staffinfo.Id);
                if (findStaff == null)
                {
                    response.ErrorMessages = new List<string>() { "invalid staff" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 404;
                    return response;
                }

                var mapUpdateDetails = _mapper.Map(staffinfo, findStaff);
                var findContactinfo = await _contactRepo.GetQueryable()
                    .FirstOrDefaultAsync(u => u.StaffId == staffinfo.Id);
                if(findContactinfo == null)
                {
                    if (staffinfo.HomeAddress != null 
                        ||staffinfo.HomePhone != null
                        ||staffinfo.MobilePhone != null 
                        ||staffinfo. Email != null)
                    {
                        var mapdata = _mapper.Map<ContactInfo>(staffinfo);
                        mapdata.StaffId = findStaff.Id;
                        await _contactRepo.Add(mapdata);
                    }
                }
                if (staffinfo.MobilePhone != null)
                {
                    findContactinfo.MobilePhone = staffinfo.MobilePhone;
                }
                if (staffinfo.HomePhone != null)
                {
                    findContactinfo.HomePhone = staffinfo.HomePhone;
                }
                if (staffinfo.Email != null)
                {
                    findContactinfo.Email = staffinfo.Email;
                }
                if (staffinfo.HomeAddress != null)
                {
                    findContactinfo.HomeAddress = staffinfo.HomeAddress;
                }
                 _contactRepo.Update(findContactinfo);
                _staffRepo.Update(mapUpdateDetails);
                await _staffRepo.SaveChanges();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "Staff info updated successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in updating staff for company" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<PaginatedGenericDto<IEnumerable<StaffInfo>>>> GetAllStaff( int pageNumber, int perPageSize, string companyid)
        {
            var response = new ResponseDto<PaginatedGenericDto<IEnumerable<StaffInfo>>>();
            try
            {

                pageNumber = pageNumber < 1 ? 1 : pageNumber;
                perPageSize = perPageSize < 1 ? 5 : perPageSize;
                var getAllStaff = _staffRepo.GetQueryable().Where(u=> u.CompanyId == companyid).Include(u=>u.ContactInformation).Select(u=>new StaffInfo
                {
                    Id = u.Id,
                    CompanyId =u.CompanyId,
                    Email = u.ContactInformation.Email,
                    FullName = u.FullName,
                    EmployeeId = u.EmployeeId,
                    DateOfBirth = u.DateOfBirth,
                    Department = u.Department,
                    DisabilityStatus = u.DisabilityStatus,
                    EmployeePhoto = u.EmployeePhoto,
                    EmploymentStatus = u.EmploymentStatus,
                    EmploymentType = u.EmploymentType,
                    EndDate = u.EndDate,
                    Ethnicity= u.Ethnicity,
                    Gender = u.Gender,
                    HomeAddress = u.ContactInformation.HomeAddress,
                    HomePhone = u.ContactInformation.HomePhone,
                    JobTitle = u.JobTitle,
                    Manager = u.Manager,
                    MaritalStatus = u.MaritalStatus,
                    MobilePhone = u.ContactInformation.MobilePhone,
                    Notes = u.Notes,
                    SocialSecurityNumber = u.SocialSecurityNumber,
                    StartDate = u.StartDate,
                    VeteranStatus = u.VeteranStatus,
                    WorkLocation = u.WorkLocation,
                });
               
                var totalCount = await getAllStaff.CountAsync();
                
                var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);
                var paginated = await getAllStaff.Skip((pageNumber - 1) * perPageSize).Take(perPageSize).ToListAsync();

                var result = new PaginatedGenericDto<IEnumerable<StaffInfo>>
                {
                    CurrentPage = pageNumber,
                    PageSize = perPageSize,
                    TotalPages = totalPages,
                    TotalStaffCount = totalCount,
                    Result = paginated
                };
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                response.Result = result;
                return response;    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting staff for company" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

    }
}
