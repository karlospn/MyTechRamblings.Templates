using System;
using System.Threading.Tasks;
using ApplicationName.Core.Extensions;
using ApplicationName.Library.Contracts;
using ApplicationName.Library.Contracts.Dto;
using ApplicationName.Library.Impl.Mappers.Extension;
using ApplicationName.Repository.Contracts;
using ApplicationName.Repository.Contracts.Model;
using Microsoft.Extensions.Logging;


namespace ApplicationName.Library.Impl
{
    public class FooService : IFooService
    {

        private readonly ILogger<FooService> _logger;
        private readonly IFooRepository _repository;

        public FooService(ILogger<FooService> logger,
                          IFooRepository repository)           
        {
            _logger = logger;
            _repository = repository;
        }


        public async Task<OperationResult<FooRsDto>> DoSomethingAsync(FooRqDto data)
        {
            var response = new OperationResult<FooRsDto>();
            try
            {
                _logger.LogInformation("Do something start!");
                
                var fooModel = await _repository.AddFooModelAsync(data.MapToModel<FooModel>());
                var dto = fooModel.MapToDto<FooRsDto>();

                _logger.LogInformation("Do something stop!");

                return response.AddResult(dto);
            }
            catch (Exception e)
            {
                _logger.LogError("Smething went wrong", e);
                return response.AddError("Something went wrong", 500, e);
            }
        }
    }
}