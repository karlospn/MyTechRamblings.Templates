using System.Threading.Tasks;

namespace ApplicationName.Function.Services
{
    public class FooService : IFooService
    {
        public async Task<string> GetFoo()
        {
            return "Foo";
        }
    }
}
