using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Persistance
{
    public interface IUserRepository
    {
        User? getUserByEmail(string email);
        void AddUser(User user);
    }
}
