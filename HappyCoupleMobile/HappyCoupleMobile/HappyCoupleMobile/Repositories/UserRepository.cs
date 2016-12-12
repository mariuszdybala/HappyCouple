using System.Threading.Tasks;
using HappyCoupleMobile.Data;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;

namespace HappyCoupleMobile.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDao _userDao;

        public UserRepository(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public async Task<User> GetUserByInternalId(int internalId)
        {
            return await _userDao.GetUserByInternalId(internalId);
        }

        public async Task<User> GetAdminUser()
        {
            return await _userDao.GetAdminUser();
        }

        public async Task InsertUser(User user)
        {
            await _userDao.InsertAsync(user);
        }

        public async Task DeleteUser(User user)
        {
            await _userDao.DeleteAsync(user);
        }
    }
}