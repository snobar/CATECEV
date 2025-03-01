using AutoMapper;

namespace CATECEV.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.AMPECO.resource.users.User, CATECEV.Models.Entity.AMPECO.Resources.User.User>()
                            .ForMember(destination => destination.AMPECOId, opt => opt.MapFrom(src => src.Id))
                            .ForMember(destination => destination.Id, opt => opt.Ignore())
                            .ReverseMap()
                            .ForMember(destination => destination.Id, opt => opt.MapFrom(src => src.AMPECOId));

            CreateMap<Models.AMPECO.resource.users.UserOptions, CATECEV.Models.Entity.AMPECO.Resources.User.UserOptions>().ReverseMap();
        }
    }
}
