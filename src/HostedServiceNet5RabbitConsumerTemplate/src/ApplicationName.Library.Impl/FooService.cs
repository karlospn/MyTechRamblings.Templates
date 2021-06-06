using System.Threading.Tasks;
using ApplicationName.Library.Contracts;
using ApplicationName.Library.Contracts.Dto;
using ApplicationName.Library.Impl.Mapper.Extension;
using ApplicationName.Repository.Contracts;
using Microsoft.Extensions.Logging;

namespace ApplicationName.Library.Impl
{
    public class FooService : IFooService
    {
        private readonly ILogger<FooService> _logger;
        private readonly IFooRepository _fooRepository;

        public FooService(ILogger<FooService> logger,
            IFooRepository fooRepository)
        {
             _fooRepository = fooRepository;
             _logger = logger;
        }

        public async Task DoSomeProcessingAsync(FooDto dto)
        {
            _logger.LogInformation("Do some more business logic!");
            await _fooRepository.AddFooModelAsync(dto.ToModel());
        }
    }
}
