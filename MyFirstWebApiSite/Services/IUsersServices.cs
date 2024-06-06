using Entities;

namespace Services
{
    public interface IUsersServices
    {
        int CheckPassword(string password);
        Task<User> GetById(int id);
        Task<User> Login(UserLogin userLogin);
        Task<User> Register(User user);
        Task<User> Update(int id, User userToUpdate);
    }
}