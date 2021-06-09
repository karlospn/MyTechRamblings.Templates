using System;
using System.Threading.Tasks;
using ApplicationName.Function.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ApplicationName.Function
{
    public class Function
    {
        private readonly ILogger<Function> _logger;
        private readonly IFooService _fooService;

        public Function(ILogger<Function> logger, 
            IFooService fooService)
        {
            _logger = logger;
            _fooService = fooService;
        }

        [FunctionName("FUNCTION-NAME")]
        public async Task Run([TimerTrigger("CRON-TIMER")]TimerInfo myTimer)
        {
            var result = await _fooService.GetFoo();
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now} and the service says: {result} ");
        }
    }
}
