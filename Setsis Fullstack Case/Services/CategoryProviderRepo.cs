using Microsoft.Extensions.Logging;
using Setsis_Fullstack_Case.Controllers;
using Setsis_Fullstack_Case.Infrastructure;
using Setsis_Fullstack_Case.Models;
using Setsis_Fullstack_Case.Models.Entity;

namespace Setsis_Fullstack_Case.Services
{
    public class CategoryProviderRepo : ICategoryProviderRepo
    {
        private SetsisFullstackCaseContext _context;

        public CategoryProviderRepo(SetsisFullstackCaseContext context)
        {
            _context = context;
        }
        public int Add(Category category)
        {
            _context.Categorys.Add(category);
            return SaveChanges();
        }
        public int Update(Category category) {

            _context.Categorys.Update(category);
            return SaveChanges();

        }
        public int GetByIdRemove(int id)
        {
            var category = GetById(id);
            _context.Categorys.Remove(category);
            return SaveChanges();
        }
        public Category GetById(int id)
        {
            return _context.Categorys.Where(c => (c.CategoryId == id)).FirstOrDefault();
        }
        public List<Category> GetByListCategory()
        {
            return _context.Categorys.ToList();
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
