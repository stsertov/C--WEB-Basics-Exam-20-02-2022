using FootballManager.ViewModels.Users;

namespace FootballManager.Contracts
{
    public interface IUserService
    {
        bool RegisterUser(RegisterUserFormModel model);

        bool UserIsValid(LogInUserFormModel model);

        string GetUsername(string userId);

        string GetUserId(LogInUserFormModel model);
    }
}
