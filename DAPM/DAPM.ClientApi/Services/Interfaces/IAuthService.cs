using DAPM.ClientApi.Models;
using System.Xml.Linq;

namespace DAPM.ClientApi.Services.Interfaces
{
    public interface IAuthService
    {
        public Guid GetUserById(Guid userId, Boolean needHash = true);
        public Guid GetUserByMail(String mail, Boolean needHash = true);
        public void PostUserToRepository(Guid id, String firstName, String lastName, String mail, Guid org, String hashPassword);
    }
}
