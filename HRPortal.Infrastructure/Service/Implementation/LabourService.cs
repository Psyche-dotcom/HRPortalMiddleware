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
    public class LabourService : ILabourService
    {

        private readonly IHRPortalRepository<Labour> _labourRepo;
        private readonly ILogger<LabourService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public LabourService(IMapper mapper,
            IConfiguration configuration,
            ILogger<LabourService> logger,
            IHRPortalRepository<Labour> labourRepo)
        {
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
            _labourRepo = labourRepo;
        }

        public async Task<ResponseDto<string>> AddLabour(AddLabourDto labourinfo, string companyid)
        {
            var response = new ResponseDto<string>();
            try
            {
                var mapLabour = _mapper.Map<Labour>(labourinfo);
                mapLabour.CompanyId = companyid;
                var labour = await _labourRepo.Add(mapLabour);
                await _labourRepo.SaveChanges();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "Labour successfully added";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in adding labour for company" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<string>> DeleteLabour(string staffid)
        {
            var response = new ResponseDto<string>();
            try
            {

                var findLabour = await _labourRepo.GetByIdAsync(staffid);
                if (findLabour == null)
                {
                    response.ErrorMessages = new List<string>() { "labour not available for deletion" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 404;
                    return response;
                }
                _labourRepo.Delete(findLabour);
                await _labourRepo.SaveChanges();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "Labour deleted successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in deleting labour for company" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<string>> UpdateLabourinfo(UpdateLabourDto labourinfo)
        {
            var response = new ResponseDto<string>();
            try
            {

                var findLabour = await _labourRepo.GetByIdAsync(labourinfo.Id);
                if (findLabour == null)
                {
                    response.ErrorMessages = new List<string>() { "invalid labour" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 404;
                    return response;
                }

                var mapUpdateDetails = _mapper.Map(labourinfo, findLabour);

                _labourRepo.Update(mapUpdateDetails);
                await _labourRepo.SaveChanges();
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "Labour info updated successfully";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in updating labour for company" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<IEnumerable<Labour>>> GetAllLabour(string companyid)
        {
            var response = new ResponseDto<IEnumerable<Labour>>();
            try
            {

                var GetLabour = await _labourRepo
                    .GetQueryable()
                    .Where(u => u.CompanyId == companyid)
                    .ToListAsync();
                response.Result = GetLabour;
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting all labour for company" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<Labour>> GetSingleLabour(string companyid, string labourid)
        {
            var response = new ResponseDto<Labour>();
            try
            {

                var GetLabour = await _labourRepo
                    .GetQueryable()
                    .FirstOrDefaultAsync(u => u.CompanyId == companyid && u.Id == labourid);
                  
                response.Result = GetLabour;
                response.StatusCode = 200;
                response.DisplayMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting labour for company" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
    }
}
