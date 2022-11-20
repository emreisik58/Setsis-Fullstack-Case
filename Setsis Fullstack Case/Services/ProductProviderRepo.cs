using Microsoft.Extensions.Logging;
using Setsis_Fullstack_Case.Controllers;
using Setsis_Fullstack_Case.Infrastructure;
using Setsis_Fullstack_Case.Models;
using Setsis_Fullstack_Case.Models.Entity;

namespace Setsis_Fullstack_Case.Services
{
    public class ProductProviderRepo : IProductProviderRepo
    {
        private SetsisFullstackCaseContext _context;

        public ProductProviderRepo(SetsisFullstackCaseContext context)
        {
            _context = context;
        }
        public int Add(Product product)
        {
            _context.Products.Add(product);
            return SaveChanges();
        }
        public int Update(Product product) {

            _context.Products.Update(product);
            return SaveChanges();

        }
        public int GetByIdRemove(int id)
        {
            var product = GetById(id);
            _context.Products.Remove(product);
            return SaveChanges();
        }
        public Product GetById(int id)
        {
            return _context.Products.Where(p => (p.ProductId == id)).FirstOrDefault();
        }
        public List<Product> GetByListProduct()
        {
            return _context.Products.ToList();
        }
        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
    }
}
