using BuberDinner.Application.Persistance;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistance
{
    public class UserrRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User? getUserByEmail(string email)
        {
            return _users.FirstOrDefault(x => x.Email == email);
        }
    }
}
