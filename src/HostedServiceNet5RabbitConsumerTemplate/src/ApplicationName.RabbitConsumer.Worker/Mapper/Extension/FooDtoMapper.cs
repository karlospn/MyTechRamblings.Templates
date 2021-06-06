using ApplicationName.Library.Contracts.Dto;
using ApplicationName.RabbitConsumer.Worker.Mapper.Profile;
using ApplicationName.RabbitConsumer.Worker.Messages;
using AutoMapper;

namespace ApplicationName.RabbitConsumer.Worker.Mapper.Extension
{
    public static class FooDtoMapper
    {
        static FooDtoMapper()
        {
            Mapper = new MapperConfiguration
                    (cfg => { cfg.AddProfile<FooDtoProfile>(); })
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static FooDto ToDto(this FooMessage model)
        {
            return model == null ? null : Mapper.Map<FooDto>(model);
        }

    }
}