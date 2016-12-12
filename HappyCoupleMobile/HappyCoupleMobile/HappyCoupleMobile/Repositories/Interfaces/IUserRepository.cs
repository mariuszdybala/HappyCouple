using System.Threading.Tasks;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByInternalId(int internalId);
        Task<User> GetAdminUser();

        Task InsertUser(User user);

        Task DeleteUser(User user);
    }
}