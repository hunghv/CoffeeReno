using System.Threading.Tasks;
using Services.Common;
using Services.ViewModels.Request;
using Services.ViewModels.Response;

namespace Services.Interfaces
{
    public interface IAdminServices
    {
        #region Blog

        IPagedResults<UserProfileResponse> GetBlogs(UserProfileDto request);

        Task<int?> SaveBlog(UserProfileModel request);

        bool DeleteCategories(int request);

        #endregion
    }
}
