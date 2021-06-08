using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationName.Core.Extensions;
using ApplicationName.Library.Contracts;
using ApplicationName.Library.Contracts.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
#if Authorization
using Microsoft.AspNetCore.Authorization;
#endif

namespace ApplicationName.WebApi.Controllers.V1
{
    /// <summary>
    /// Foo service
    /// </summary>
#if Authorization
    [Authorize]
#endif
    [ApiVersion("1.0")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorResult),  StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResult),  StatusCodes.Status500InternalServerError)]
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class MyServiceController : ControllerBase
    {
        private readonly ILogger<MyServiceController> _logger;
        private readonly IFooService _fooService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="fooService"></param>
        public MyServiceController(ILogger<MyServiceController> logger,
            IFooService fooService)
        {
            _logger = logger;
            _fooService = fooService;
        }


        /// <summary>
        /// Get All Foo Data
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<FooRsDto>), StatusCodes.Status200OK)]
        public Task<ActionResult> Get()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Get Foo Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FooRsDto), StatusCodes.Status200OK)]
        public Task<ActionResult> Get(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create Foo Data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(FooRsDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> Post(FooRqDto request)
        {
            try
            {
                var response = await _fooService.DoSomethingAsync(request);
                if (!response.HasErrors)
                {
                    return Ok(response.Result);
                }
                if (response.Result == null && !response.HasErrors)
                    return NoContent();
                return BadRequest(response.Errors);

            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }
        
        /// <summary>
        /// Update Foo Data
        /// </summary>
        /// <param name="request">Foo data request update</param>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FooRsDto), StatusCodes.Status200OK)]
        public Task<ActionResult> Put(FooRqDto request)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Delete Foo Data
        /// </summary>
        /// <param name="id">Foo data id</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public Task<ActionResult> Delete(string id)
        {
            throw new NotImplementedException();
        }
        
    }
}