using System;
using System.Threading.Tasks;
using HappyCoupleMobile.Model;
using HappyCoupleMobile.Repositories.Interfaces;

namespace HappyCoupleMobile.Services
{
    public class SimpleAuthService : ISimpleAuthService
    {
        private readonly IUserRepository _userRepository;

        private readonly User _userMockedUser;
        public User Admin { get; private set; }

        public SimpleAuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            _userMockedUser = new User { InternalId = 1, IsAdmin = true, Name = "Kasia" };

        }

        public async Task AddNewUser(User user)
        {
            await _userRepository.InsertUser(user);
        }

        public async Task LogIn()
        {
            if (Admin != null)
            {
                return;
            }

            Admin = await _userRepository.GetAdminUser();

            if (Admin == null)
            {
                await  _userRepository.InsertUser(_userMockedUser);
                Admin = await _userRepository.GetAdminUser();
            }
        }
    }
}