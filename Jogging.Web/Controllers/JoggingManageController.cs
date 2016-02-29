using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Jogging.Web.DAL;
using Jogging.Web.Models;
using Jogging.Web.ViewModel;

namespace Jogging.Web.Controllers
{
    public class JoggingManageController : ApiController
    { 
        private readonly JoggingRepository _joggingRepository;
        private readonly IMapper _mapper;

        public JoggingManageController(IMapper mapper, JoggingRepository joggingRepository)
        {
            _mapper = mapper;
            _joggingRepository = joggingRepository;
        }
        
        public IEnumerable<JoggingItemViewModel> GetJoggings(int userId)
        {
            var joggingItems = _joggingRepository.GetJoggingForUser(userId);
            return joggingItems.Select(j => _mapper.Map<JoggingItemViewModel>(j));
        }

        public JoggingItemViewModel GetJogging(int userId, int joggingId)
        {
            var joggingItem = _joggingRepository.Get(joggingId);
            return _mapper.Map<JoggingItemViewModel>(joggingItem);
        }
        
        public JoggingItemViewModel PostJogging(int userId, JoggingItemViewModel item)
        {
            var jogging = _mapper.Map<JoggingItem>(item);
            var newJogging = _joggingRepository.Add(jogging);
            return _mapper.Map<JoggingItemViewModel>(newJogging);
        }
        
        public void PutJogging(int userId, JoggingItemViewModel item)
        {
            var jogging = _mapper.Map<JoggingItem>(item);
            _joggingRepository.Update(jogging);
        }
        
        public void DeleteJogging(int id)
        {
            _joggingRepository.Remove(id);
        }
    }
}