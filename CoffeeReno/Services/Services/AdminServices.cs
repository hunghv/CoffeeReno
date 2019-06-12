using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Repository.Interfaces;
using Services.Common;
using Services.Interfaces;
using Services.ViewModels.Request;
using Services.ViewModels.Response;

namespace Services.Services
{
    public class AdminServices : PaggingHelper, IAdminServices
    {
        #region Declare Property

        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructure

        public AdminServices(IUserProfileRepository userProfileRepository, IMapper mapper)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
        }

        #endregion

        #region Public Method 
        public IPagedResults<UserProfileResponse> GetBlogs(UserProfileDto request)
        {
            var response = new PagedResults<UserProfileResponse>();
            var query = _userProfileRepository.GetAllNoneDeleted();
            response.Total = query.Count();

            //sort  data
            if (!string.IsNullOrEmpty(request?.SortField))
            {
                OrderBy(ref query, request);
            }
            else
            {
                query = query.OrderBy(x => x.DisplayName);
            }

            if (request?.Skip != null && request.Take.HasValue)
            {
                Paging(ref query, request);
            }

            var result = _mapper.Map<List<UserProfileResponse>>(query.ToList());

            response.Data = result;
            return response;
        }

        public async Task<int?> SaveBlog(UserProfileModel request)
        {
            if (request.Id != null)
            {
                var blog = _userProfileRepository.GetSingleNoneDeleted(x => x.Id == request.Id);
                //update
                if (blog != null)
                {
                    blog.DisplayName = request.Name;
                    blog.ModifiedDate = Constants.GetDateNow();
                    blog.ModifiedBy = Constants.GetUserId();
                    _userProfileRepository.Update(blog);
                    await _userProfileRepository.CommitAsync();
                }
                if (blog != null) return blog.Id;
            }
            else
            {
                //add new
                var userProfile = new UserProfile
                {
                    DisplayName = request.Name,
                    CreatedBy = Constants.GetUserId(),
                    ModifiedBy = Constants.GetUserId(),
                    CreatedDate = Constants.GetDateNow(),
                    ModifiedDate = Constants.GetDateNow()
                };
                _userProfileRepository.Add(userProfile);
                await _userProfileRepository.CommitAsync();
                return userProfile.Id;
            }
            return null;
        }

        public bool DeleteCategories(int request)
        {
            var blog = _userProfileRepository.GetSingleNoneDeleted(x => x.Id == request);
            if (blog != null)
            {
                blog.IsDeleted = true;
                blog.DeletedBy = Constants.GetUserId();
                blog.DeletedDate = Constants.GetDateNow();
                _userProfileRepository.Update(blog);
                return _userProfileRepository.Commit();
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Private Method


        #endregion
    }
}
