using AutoMapper;
using Ecommerce.Core.DTOs;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.AutpMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(x => x.PositionId, opt => opt.MapFrom(origin => new Guid(origin.PositionId)));
            CreateMap<User, UserPosition>();
        }
    }
}
