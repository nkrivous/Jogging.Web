using System.Linq;
using AutoMapper;
using Jogging.Web.Models;
using Jogging.Web.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Jogging.Web.Infrastructure
{
    public class AutoMapperProfileConfiguration:Profile
    {
        protected override void Configure()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            CreateMap<JoggingItem, JoggingItemViewModel>();
            CreateMap<JoggingItemViewModel, JoggingItem>();

            CreateMap<IdentityRole, string>().ConvertUsing(new UserRoleConverter());
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(x => x.Role, opts => opts.MapFrom(src => roleManager.FindById(src.Roles.First().RoleId).Name))
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.UserName));
            CreateMap<UserViewModel, ApplicationUser>();
        }
    }

    public class UserRoleConverter : ITypeConverter<IdentityRole, string>
    {
        public string Convert(ResolutionContext context)
        {
            // Assuming a "Name" property on UserRole
            return ((IdentityRole)context.SourceValue).Name;
        }
    }
}