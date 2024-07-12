using HealthcareManagementSystem.DB;
using HealthcareManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Services
{
    public interface IUsersService
    {
        Task<List<UsersModel>> GetAllUsers();
    }
    public class UsersService:IUsersService
    {
        public HM_dbContext _ObjDBContext;
        public UsersService(HM_dbContext ObjDBContext) 
        {
            _ObjDBContext = ObjDBContext;
        }
        public async Task<List<UsersModel>> GetAllUsers()
        {
            return await _ObjDBContext.Users.ToListAsync();
        }
    }
}
