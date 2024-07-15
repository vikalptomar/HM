using Microsoft.AspNetCore.Mvc.Filters;

namespace HealthcareManagementSystem.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AllowAnonymousAttribute : Attribute
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            int a = 2;
            return;
        }
    }
}
