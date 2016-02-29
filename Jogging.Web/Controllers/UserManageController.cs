using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Jogging.Web.Interfaces;
using Jogging.Web.Models;
using Jogging.Web.ViewModel;

namespace Jogging.Web.Controllers
{
    public class UserManageController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManageController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            var users = _userRepository.GetAll();
            return users.Select(user => _mapper.Map<UserViewModel>(user));
        }

        public UserViewModel GetUser(int id) {
            var user = _userRepository.Get(id);
            return _mapper.Map<UserViewModel>(user);
        }

        public UserViewModel PostUser(UserViewModel item)
        {
            var user = _mapper.Map<ApplicationUser>(item);
            var newUser = _userRepository.Add(user);
            return _mapper.Map<UserViewModel>(newUser);
        }

        public void PutUser(UserViewModel item)
        {
            var user = _mapper.Map<ApplicationUser>(item);
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Remove(id);
        }
    }
}