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
        private readonly ILogger<UserProviderRepo> _logger;

        public UserProviderRepo(SetsisFullstackCaseContext context, ILogger<UserProviderRepo> logger)
        {
            _context = context;
            _logger = logger;
        }
        public int Add(User user)
        {
            _context.Users.Add(user);
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
