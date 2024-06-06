using Entities;
using Repositories;


namespace Services
{
    public class UsersServices : IUsersServices
    {
        private IUsersRepository _userRepository;

        public UsersServices(IUsersRepository usersRepository)
        {
            _userRepository = usersRepository;
        }

        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;

        }


        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<User> Register(User user)
        {

            if (CheckPassword(user.Password) <= 2)
                return null;
            return await _userRepository.Register(user);
        }

        public async Task<User> Login(UserLogin userLogin)
        {
            return await _userRepository.Login(userLogin);
        }

        public async Task<User> Update(int id, User userToUpdate)
        {
            return await _userRepository.Update(id, userToUpdate);
        }

    }
}

