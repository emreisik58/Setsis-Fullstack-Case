using Microsoft.EntityFrameworkCore;
using Setsis_Fullstack_Case.Models.Entity;

namespace Setsis_Fullstack_Case.Infrastructure
{
    public interface IUserProviderRepo 
    {
        public int Add(User user);
        public int GetByIdRemove(int id);
        public User GetByUserName(string UserName);
        public User GetById(int id);
        public List<User> GetByListUser();
        public bool UserRequired(string UserName, string password);

    }
}
