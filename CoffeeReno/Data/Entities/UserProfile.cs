using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.Entities.Base;

namespace Data.Entities
{
    public class UserProfile : Entity
    {
        public UserProfile()
        {
            UserRoles = new List<UserRole>();
        }

        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string ConfirmPassword { get; set; }
        [MaxLength(255)]
        public string DisplayName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string NickName { get; set; }
        public byte[] ThumbnailPhoto { get; set; }
        public string Domain { get; set; }
        public bool? ReceiveEmail { get; set; }
        public bool? Active { get; set; }
        [MaxLength(30)]
        public string TelephoneNumber { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
    public class UserRole : Entity
    {
        public int UserProfileId { get; set; }
        public int RoleId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual Role Role { get; set; }
    }
    public class Role : Entity
    {
        public Role()
        {
            UserRoles = new List<UserRole>();
        }

        [MaxLength(256)]
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }

    public class UserLoginHistory : Entity
    {
        public int UserId { get; set; }
        public virtual UserProfile User { get; set; }
        public Guid AccessToken { get; set; }
        public bool IsLoggedOut { get; set; }
        public bool IsAppToken { get; set; }
    }
}
