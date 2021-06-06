using System.Threading.Tasks;

namespace ApplicationName.Function.Services
{
    public interface IFooService
    {
        Task<string> GetFoo();
    }
}
