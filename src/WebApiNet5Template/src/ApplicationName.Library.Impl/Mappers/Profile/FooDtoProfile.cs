using ApplicationName.Library.Contracts.Dto;
using ApplicationName.Repository.Contracts.Model;

namespace ApplicationName.Library.Impl.Mappers.Profile
{
    public class FooDtoProfile : AutoMapper.Profile
    {
        public FooDtoProfile()
        {
            CreateMap<FooModel,FooRsDto>()
                .ForMember(d => d.Data, opt => opt.MapFrom(m => m.FooData));

            CreateMap<FooRqDto,FooModel >()
                .ForMember(d=>d.FooData, opt => opt.MapFrom(m=>m.Data));
     
        }
    }
}