using ApplicationName.Library.Contracts.Dto;
using ApplicationName.Repository.Contracts.Model;

namespace ApplicationName.Library.Impl.Mapper.Profile
{
    public class FooModelProfile : AutoMapper.Profile
    {
        public FooModelProfile()
        {
            CreateMap<FooDto, FooModel>()
                .ForMember(model => model.FooData, opt => opt.MapFrom(entity => entity.Data));
        }
    }
}