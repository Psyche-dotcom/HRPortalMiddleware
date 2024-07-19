using AutoMapper;
using HRPortal.Core.DTO;
using HRPortal.Core.Entities;

namespace HRPortal.HRPortalProfile
{
    public class HRPortalProjectProfile : Profile
    {
        public HRPortalProjectProfile()
        {
            CreateMap<Staff, AddStaffDto>().ReverseMap();
            CreateMap<ContactInfo, AddStaffDto>().ReverseMap();
            CreateMap<UpdateStaffDto, ContactInfo>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateStaffDto, Staff>().ReverseMap();
        }
    }
}
