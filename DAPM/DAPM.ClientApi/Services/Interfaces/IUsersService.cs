using DAPM.ClientApi.Models;
using System.Xml.Linq;

namespace DAPM.ClientApi.Services.Interfaces
{
    public interface IUsersService
    {
        public Guid GetAllUsers(Guid managerId);
        public Guid AcceptUser(Guid ManagerId, Guid userId, int newStatus);
        public Guid RemoveUser(Guid ManagerId, Guid userId);
    }
}
