using DAPM.ClientApi.Models;
using System.Xml.Linq;

namespace DAPM.ClientApi.Services.Interfaces
{
    public interface IAuthService
    {
        public Guid GetUserById(Guid organizationId);
        public Guid GetUserByMail(String mail);
        public Guid PostUserToRepository(Guid id, String firstName, String lastName, String mail, Guid org, String hashPassword);
    }
}
