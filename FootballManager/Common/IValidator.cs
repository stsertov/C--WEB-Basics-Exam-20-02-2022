using FootballManager.ViewModels.Players;
using FootballManager.ViewModels.Users;

namespace FootballManager.Common
{
    public interface IValidator
    {
        bool ValidateUser(RegisterUserFormModel model);
        bool ValidatePlayer(AddPlayerFormModel model);
    }
}
