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
            Posts = new List<Post>();
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
        public ICollection<Post> Posts { get; set; }
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

        [MaxLength(255)]
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


    public class AdsType : Entity
    {
        public AdsType()
        {
            AdsFroms = new List<AdsForm>();
        }

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }

        public ICollection<AdsForm> AdsFroms { get; set; }
    }

    public class AdsForm: Entity
    {
        public AdsForm()
        {
            Posts = new List<Post>();
        }

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }

        public virtual int AdsTypeId { get; set; }
        public virtual AdsType AdsType { get; set; }

        public ICollection<Post> Posts { get; set; }
    }

    public class Image : Entity
    {
        public Image()
        {
            PostImages = new List<PostImage>();
        }

        public string ImageUrl { get; set; }

        public ICollection<PostImage> PostImages { get; set; }
    }

    public class PostType : Entity
    {
        public PostType()
        {
            Posts = new List<Post>();
        }

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }

        public ICollection<Post> Posts { get; set; }
    }

    public class Post : Entity
    {
        public Post()
        {
            PostImages = new List<PostImage>();
        }

        public int PostTypeId { get; set; }
        public virtual PostType PostType { get; set; }
        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public int AdsFormId { get; set; }
        public virtual AdsForm AdsForm { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }
        public float Price { get; set; }
        [MaxLength(50)]
        public string PriceDescription { get; set; }

        //TODO: Location

        [MaxLength(50)]
        public string Property1 { get; set; } //Not happy about this
        [MaxLength(50)]
        public string Property2 { get; set; } //Not happy about this

        public string Description { get; set; }


        public ICollection<PostImage> PostImages { get; set; }
    }

    public class PostImage : Entity
    {
        public int ImageId { get; set; }
        public virtual Image Image { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
