using ITI.E_Commerce.Api.Model;
using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Repositories
{
    public interface IOrderRepositoryApi
    {
        public Task<GetData> Edit(int id_order, OrderCreateModelApi obj);
        public GetData Delete(int id);
        public Task<GetData> CreateNew(OrderCreateModelApi obj);
        public GetData Get(string id_user);
        public GetData GetProductByCaterogyName(string name);
        public IEnumerable<ProductModel> get_best_seller();
    }
}
