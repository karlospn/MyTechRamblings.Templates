using System.Threading.Tasks;
using ApplicationName.Repository.Contracts.Model;

namespace ApplicationName.Repository.Contracts
{
    public interface IFooRepository
    {
       Task<FooModel> AddFooModelAsync(FooModel dataModel);

    }
}