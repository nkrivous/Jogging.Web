using System.Collections.Generic;
using Jogging.Web.Models;

namespace Jogging.Web.Interfaces
{
    public interface IJoggingRepository : IRepository<JoggingItem>
    {
        IEnumerable<JoggingItem> GetJoggingForUser(int userId);
        IEnumerable<JoggingItem> GetJoggingForUser(ApplicationUser user);
    }
}