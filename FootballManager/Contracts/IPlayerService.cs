using FootballManager.ViewModels.Players;
using System.Collections.Generic;

namespace FootballManager.Contracts
{
    public interface IPlayerService
    {
        bool AddPlayer(AddPlayerFormModel model, string userId);

        IEnumerable<PlayerViewModel> GetAllPlayers();

        IEnumerable<PlayerViewModel> GetAllPlayersByUser(string userId);

        void AddPlayerToCollection(int playerId, string userId);

        void RemovePlayerFromCollection(int playerId, string userId);
    }
}
