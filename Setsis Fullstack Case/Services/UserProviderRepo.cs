using Microsoft.Extensions.Logging;
using Setsis_Fullstack_Case.Controllers;
using Setsis_Fullstack_Case.Infrastructure;
using Setsis_Fullstack_Case.Models;
using Setsis_Fullstack_Case.Models.Entity;

namespace Setsis_Fullstack_Case.Services
{
    public class UserProviderRepo : IUserProviderRepo
    {
        private SetsisFullstackCaseContext _context;

        public UserProviderRepo(SetsisFullstackCaseContext context)
        {
            _context = context;
        }
        public int Add(User user)
        {
            _context.Users.Add(user);
            return SaveChanges();
        }
        public int Update(User user) {

            _context.Users.Update(user);
            return SaveChanges();

        }
        public int GetByIdRemove(int id)
        {
            var user = GetById(id);
            _context.Users.Remove(user);
            return SaveChanges();
        }
        public User GetByUserName(string UserName)
        {
            return _context.Users.Where(U => (U.userName == UserName)).FirstOrDefault();
        }
        public User GetById(int id)
        {
            return _context.Users.Where(U => (U.UserId == id)).FirstOrDefault();
        }
        public List<User> GetByListUser()
        {
            return _context.Users.ToList();
        }
        public bool UserRequired(string UserName, string password)
        {
            return _context.Users.Where(U => (U.userName == UserName && U.password == password)).Count() > 0 ? true : false;
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
