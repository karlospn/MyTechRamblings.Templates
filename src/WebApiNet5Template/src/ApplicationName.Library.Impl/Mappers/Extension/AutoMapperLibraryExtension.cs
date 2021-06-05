using ApplicationName.Library.Impl.Mappers.Profile;
using AutoMapper;

namespace ApplicationName.Library.Impl.Mappers.Extension
{
    public static class AutoMapperLibraryExtension
    {
        static AutoMapperLibraryExtension()
        {
            Mapper = new MapperConfiguration
                (cfg =>
                {
                    cfg.AddProfile<FooDtoProfile>();

                })
                .CreateMapper();
        }

        public static T MapToDto<T>(this object model) where T : class
        {
            return model == null ? null : Mapper.Map<T>(model);
        }
        public static T MapToModel<T>(this object entity) where T : class
        {
            return entity == null ? null : Mapper.Map<T>(entity);
        }
        
        internal static IMapper Mapper { get; }

       
    }
}