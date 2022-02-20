using FootballManager.Contracts;
using FootballManager.ViewModels.Players;
using MyWebServer.Controllers;
using MyWebServer.Http;

namespace FootballManager.Controllers
{
    public class PlayersController : Controller
    {
        private IPlayerService playerService;

        public PlayersController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [Authorize]
        public HttpResponse Add()
            => View();

        [HttpPost]
        [Authorize]
        public HttpResponse Add(AddPlayerFormModel model)
        {
            bool isAdded = playerService.AddPlayer(model, User.Id);
            if (!isAdded)
                return View();

            return Redirect("/Players/All");
        }

        [Authorize]
        public HttpResponse All()
            => View(playerService.GetAllPlayers());
        

        [Authorize]
        public HttpResponse Collection()
            => View(playerService.GetAllPlayersByUser(User.Id));

        [Authorize]
        public HttpResponse AddToCollection(int playerId)
        {
            playerService
                .AddPlayerToCollection(playerId, User.Id);
            return Redirect("/Players/All");
        }

        [Authorize]
        public HttpResponse RemoveFromCollection(int playerId)
        {
            playerService
                .RemovePlayerFromCollection(playerId, User.Id);
            return Redirect("/Players/Collection");
        }
    }
}
