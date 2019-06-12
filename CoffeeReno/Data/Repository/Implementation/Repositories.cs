using Data.Context;
using Data.Entities;
using Data.Repository.Base;
using Data.Repository.Interfaces;

namespace Data.Repository.Implementation
{
    public class UserLoginHistoryRepository : EntityBaseRepository<UserLoginHistory>, IUserLoginHistoryRepository
    {
        public UserLoginHistoryRepository(CoffeeRenoContext context) : base(context)
        {
        }
    }
    public class UserProfileRepository : EntityBaseRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(CoffeeRenoContext context) : base(context)
        {
        }
    }
    public class UserRoleRepository : EntityBaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(CoffeeRenoContext context) : base(context)
        {
        }
    }
    public class RoleRepository : EntityBaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(CoffeeRenoContext context) : base(context)
        {
        }
    }
}
