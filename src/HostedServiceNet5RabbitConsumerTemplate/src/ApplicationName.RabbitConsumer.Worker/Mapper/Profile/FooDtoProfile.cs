using ApplicationName.Library.Contracts.Dto;
using ApplicationName.RabbitConsumer.Worker.Messages;

namespace ApplicationName.RabbitConsumer.Worker.Mapper.Profile
{
    public class FooDtoProfile : AutoMapper.Profile
    {
        public FooDtoProfile()
        {
            CreateMap<FooMessage, FooDto>()
                .ForMember(model => model.Data, opt => opt.MapFrom(entity => entity.Payload));
        }
    }
}