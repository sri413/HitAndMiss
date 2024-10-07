using Microsoft.AspNetCore.Mvc;
using UROC.HitAndMiss.Core.Interfaces;
using UROC.HitAndMiss.Core.Models;
using UROC.HitAndMiss.Core.Responses;

namespace Uroc.HitAndMiss_API.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private static IHitAndMissModel _gameModel;

        public GameController() {
            _gameModel = new HitAndMissModel();
        }
        [HttpGet("GameDetails")]
        public async Task<ActionResult<HitAndMissGameResponse>> GetGame(long gameId)
        {
            return await _gameModel.GetGameDetails(gameId);
        }

        //CREATE ANOHTER API ENDPOINT HERE THAT TAKES IN THE HitAndMissPlayRequest and returns a newly created HitAndMissResultResponse with the base data in for the game output.
        // THIS can be consumed by a front end as JSON, or reserviced to return anything we need to output.
    }
}
