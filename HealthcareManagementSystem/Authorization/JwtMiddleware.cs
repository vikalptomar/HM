using HealthcareManagementSystem.Controllers;
using HealthcareManagementSystem.DB;
using HealthcareManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace HealthcareManagementSystem.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, HM_dbContext _dbContext, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var accountId = jwtUtils.ValidateJwtToken(token);

            if (accountId != null)
            {
                // attach account to context on successful jwt validation
                
                context.Items["Account"] = await _dbContext.Users.Where(x => x.UserId == accountId.Value).Select(x => new UsersModel()
                {
                    UserId = (int)x.UserId,
                    UserName = x.UserName,
                    UserPassword = x.UserPassword,
                    UserEmail = x.UserEmail
                }).FirstOrDefaultAsync();
            }
            await _next(context);
        }
    }
}
