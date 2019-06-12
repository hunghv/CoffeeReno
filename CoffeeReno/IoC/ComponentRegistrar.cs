using Data.Context;
using Data.Repository.Implementation;
using Data.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Services;

namespace IoC
{
    public class ComponentRegistrar
    {
        public static void InitComponent(IServiceCollection services)
        {
            services.AddScoped(typeof(CoffeeRenoContext), typeof(CoffeeRenoContext));
            services.AddScoped(typeof(IUserLoginHistoryRepository), typeof(UserLoginHistoryRepository));
            services.AddScoped(typeof(IUserProfileRepository), typeof(UserProfileRepository));
            services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            services.AddScoped(typeof(IUserRoleRepository), typeof(UserRoleRepository));
            services.AddScoped(typeof(IAdminServices), typeof(AdminServices));
        }
    }
}
