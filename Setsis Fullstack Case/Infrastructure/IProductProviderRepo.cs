using Setsis_Fullstack_Case.Models.Entity;

namespace Setsis_Fullstack_Case.Infrastructure
{
    public interface IProductProviderRepo
    {
        public int Add(Product product);
        public int Update(Product product);
        public int GetByIdRemove(int id);
        public Product GetById(int id);
        public List<Product> GetByListProduct();
    }
}
