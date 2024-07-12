using HealthcareManagementSystem.DB;
using HealthcareManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Services
{
    public interface IAuthService
    {
        Task<UsersModel> Login(string username, string password);
    }
    public class AuthService : IAuthService
    {
        public HM_dbContext _ObjDbContext;
        public AuthService(HM_dbContext objDbContext)
        {
            _ObjDbContext = objDbContext;
        }

        public async Task<UsersModel> Login(string useremail, string password)
        {
            var currentUser = await _ObjDbContext.Users.Where(u => u.UserEmail == useremail && u.UserPassword == password).FirstOrDefaultAsync();
            if (currentUser != null)
            {

                return currentUser;

            }
            else
            {
                return new UsersModel();
            }
        }
    }
}
