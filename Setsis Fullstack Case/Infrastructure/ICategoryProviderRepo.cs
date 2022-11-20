using Microsoft.EntityFrameworkCore;
using Setsis_Fullstack_Case.Models.Entity;

namespace Setsis_Fullstack_Case.Infrastructure
{
    public interface ICategoryProviderRepo
    {

        public int Add(Category category);
        public int Update(Category category);
        public int GetByIdRemove(int id);
        public Category GetById(int id);
        public List<Category> GetByListCategory();


    }
}
