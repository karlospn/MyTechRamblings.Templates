using System.Threading.Tasks;
using ApplicationName.Core.Extensions;
using ApplicationName.Library.Contracts.Dto;

namespace ApplicationName.Library.Contracts
{
    /// <summary>
    /// Your Service
    /// </summary>
    public interface IFooService
    {
        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<OperationResult<FooRsDto>> DoSomethingAsync(FooRqDto data);
    }
}