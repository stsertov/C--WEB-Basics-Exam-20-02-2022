using FootballManager.Common;
using FootballManager.Contracts;
using FootballManager.Data.Common;
using FootballManager.Data.Models;
using FootballManager.ViewModels.Players;
using System.Collections.Generic;
using System.Linq;

namespace FootballManager.Services
{
    public class PlayerService : IPlayerService
    {
        private IValidator validator;
        private IRepository repository;

        public PlayerService(IValidator validator,
            IRepository repository)
        {
            this.validator = validator;
            this.repository = repository;
        }
        public bool AddPlayer(AddPlayerFormModel model, string userId)
        {
            bool isValid = validator.ValidatePlayer(model);

            if (isValid)
            {
                Player player = new Player
                {
                    FullName = model.FullName,
                    ImageUrl = model.ImageUrl,
                    Position = model.Position,
                    Speed = model.Speed,
                    Endurance = model.Endurance,
                    Description = model.Description
                };

                repository.Add<Player>(player);
                repository.Add<UserPlayer>(new UserPlayer
                {
                    Player = player,
                    UserId = userId
                });

                repository.SaveChanges();
            }

            return isValid;
        }

        public IEnumerable<PlayerViewModel> GetAllPlayers()
            => repository.All<Player>()
               .Select(p => new PlayerViewModel
               {
                   Id = p.Id,
                   ImageUrl = p.ImageUrl,
                   FullName = p.FullName,
                   Position = p.Position,
                   Speed = p.Speed,
                   Endurance = p.Endurance,
                   Description = p.Description
               }).ToList();

        public IEnumerable<PlayerViewModel> GetAllPlayersByUser(string userId)
            => repository.All<UserPlayer>()
               .Where(up => up.UserId == userId)
               .Select(up => new PlayerViewModel
               {
                   Id = up.PlayerId,
                   ImageUrl = up.Player.ImageUrl,
                   FullName = up.Player.FullName,
                   Position = up.Player.Position,
                   Speed = up.Player.Speed,
                   Endurance = up.Player.Endurance,
                   Description = up.Player.Description
               }).ToList();

        public void AddPlayerToCollection(int playerId, string userId)
        {
            UserPlayer userPlayer = GetUserPlayer(playerId, userId);

            if(userPlayer == null)
            {
                repository.Add(new UserPlayer
                {
                    PlayerId = playerId,
                    UserId = userId
                });
                repository.SaveChanges();
            }
        }

        public void RemovePlayerFromCollection(int playerId, string userId)
        {
           UserPlayer userPlayer = GetUserPlayer(playerId, userId);

            if(userPlayer != null)
            {
                repository.Remove(userPlayer);
                repository.SaveChanges();
            }
        }

        private UserPlayer GetUserPlayer(int playerId, string userId)
            => repository.All<UserPlayer>()
                .FirstOrDefault(up => up.UserId == userId && up.PlayerId == playerId);
    }
}
