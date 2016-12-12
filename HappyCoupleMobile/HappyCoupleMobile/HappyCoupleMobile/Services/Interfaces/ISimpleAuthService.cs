using System.Threading.Tasks;
using HappyCoupleMobile.Model;

namespace HappyCoupleMobile.Services
{
    public interface ISimpleAuthService
    {
        User Admin { get; }
        Task AddNewUser(User user);
        Task LogIn();
    }
}