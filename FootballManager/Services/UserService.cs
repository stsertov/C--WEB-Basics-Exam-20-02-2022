using FootballManager.Common;
using FootballManager.Contracts;
using FootballManager.Data.Common;
using FootballManager.Data.Models;
using FootballManager.ViewModels.Users;
using System.Linq;

namespace FootballManager.Services
{
    public class UserService : IUserService
    {
        private IValidator validator;
        private IRepository repository;
        private IHasher hasher;

        public UserService(IValidator validator,
            IRepository repository,
            IHasher hasher)
        {
            this.validator = validator;
            this.repository = repository;
            this.hasher = hasher;
        }

        public bool RegisterUser(RegisterUserFormModel model)
        {
            bool isValidUser = validator.ValidateUser(model);

            if (repository.All<User>().Any(u => u.Username == model.Username))
            {
                isValidUser = false;
            }

            if (repository.All<User>().Any(u => u.Email == model.Email))
            {
                isValidUser = false;
            }

            if(isValidUser)
            {
                repository.Add<User>(new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = hasher.GetHash(model.Password)
                });
                repository.SaveChanges();
            }

            return isValidUser;
        }

        public bool UserIsValid(LogInUserFormModel model)
           => repository.All<User>().Any(u =>
                      u.Username == model.Username &&
                      u.Password == hasher.GetHash(model.Password));

        public string GetUsername(string userId)
           => repository.All<User>()
              .FirstOrDefault()?.Username;

        public string GetUserId(LogInUserFormModel model)
           => repository.All<User>()
              .FirstOrDefault(u => u.Username == model.Username).Id;
      
    }
}
