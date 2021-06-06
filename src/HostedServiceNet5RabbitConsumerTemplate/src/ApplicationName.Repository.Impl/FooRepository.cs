using System.Threading.Tasks;
using ApplicationName.Repository.Contracts;
using ApplicationName.Repository.Contracts.Model;

namespace ApplicationName.Repository.Impl
{
    public class FooRepository : IFooRepository
    {
        public async Task AddFooModelAsync(FooModel model)
        {
            //Do whatever
        }
    }
}