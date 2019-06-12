using AutoMapper;
using Data.Entities;
using Services.ViewModels.Response;

namespace Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserProfile, UserProfileResponse>()
                .ForMember(d => d.Name, s => s.MapFrom(t => t.DisplayName))
                .ForMember(d => d.CreatedBy, s => s.MapFrom(t => t.CreatedBy))
                .ForMember(d => d.CreatedDate, s => s.MapFrom(t => t.CreatedDate))
                .ForMember(d => d.Id, s => s.MapFrom(t => t.Id))
                .ForMember(d => d.IsDeleted, s => s.MapFrom(t => t.IsDeleted))
                .ForMember(d => d.ModifiedBy, s => s.MapFrom(t => t.ModifiedBy))
                .ForMember(d => d.ModifiedDate, s => s.MapFrom(t => t.ModifiedDate));
        }
    }
}
