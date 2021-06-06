using ApplicationName.Library.Contracts.Dto;
using ApplicationName.Library.Impl.Mapper.Profile;
using ApplicationName.Repository.Contracts.Model;
using AutoMapper;

namespace ApplicationName.Library.Impl.Mapper.Extension
{
    public static class FooModelMapper
    {
        static FooModelMapper()
        {
            Mapper = new MapperConfiguration
                    (cfg => { cfg.AddProfile<FooModelProfile>(); })
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static FooModel ToModel(this FooDto dto)
        {
            return dto == null ? null : Mapper.Map<FooModel>(dto);
        }

    }
}