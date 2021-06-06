using System.Threading.Tasks;
using ApplicationName.Library.Contracts.Dto;

namespace ApplicationName.Library.Contracts
{
    public interface IFooService
    {
        Task DoSomeProcessingAsync(FooDto dto);
    }
}
