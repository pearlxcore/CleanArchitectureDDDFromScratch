using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interface.Authentication;
using BuberDinner.Application.Persistance;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator=jwtTokenGenerator;
            _userRepository=userRepository;
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            // check user if exists
            if (_userRepository.getUserByEmail(email) is not User user)
            {
                return Errors.Authentication.InvalidCredential;
            }

            // validate password
            if (user.Password != password)
            {
                return new[] { Errors.Authentication.InvalidCredential };
            }
            // create jwt token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            // check if user exist
            if (_userRepository.getUserByEmail(email) != null)
            {
                return Errors.User.DuplicateEmail;
            }

            // create user
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };
            _userRepository.AddUser(user);

            // generate unique id
            string token = _jwtTokenGenerator.GenerateToken(user);

            // create jwt token
            return new AuthenticationResult(user, token);
        }
    }
}
