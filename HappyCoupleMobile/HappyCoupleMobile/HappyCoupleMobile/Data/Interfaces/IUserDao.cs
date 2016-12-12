using System.Threading.Tasks;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Data
{
    public interface IUserDao : IBaseDao<User>
    {
        Task<User> GetUserByInternalId(int internalId);
        Task<User> GetAdminUser();
    }
}