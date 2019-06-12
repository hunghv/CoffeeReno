using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.Repository.Base;

namespace Data.Repository.Interfaces
{
    public interface IUserLoginHistoryRepository : IEntityBaseRepository<UserLoginHistory> { }
    public interface IUserRoleRepository : IEntityBaseRepository<UserRole> { }
    public interface IUserProfileRepository : IEntityBaseRepository<UserProfile> { }
    public interface IRoleRepository : IEntityBaseRepository<Role> { }

}
