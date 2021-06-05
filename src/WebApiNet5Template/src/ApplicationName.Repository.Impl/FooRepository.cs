using System;
using System.Threading.Tasks;
using ApplicationName.Repository.Contracts;
using ApplicationName.Repository.Contracts.Model;

namespace ApplicationName.Repository.Impl
{
    public class FooRepository : IFooRepository
    {
       
        public async Task<FooModel> AddFooModelAsync(FooModel data)
        {
            return Invoke();
        }
        
        private FooModel Invoke()
        {
            return new FooModel()
            {
                FooData = $"Your data at: {DateTime.Now:D}"
            };
        }
    }
}