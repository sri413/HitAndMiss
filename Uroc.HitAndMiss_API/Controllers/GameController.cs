using Microsoft.AspNetCore.Mvc;
using UROC.HitAndMiss.Core.Interfaces;
using UROC.HitAndMiss.Core.Models;
using UROC.HitAndMiss.Core.Responses;
using UROC.HitAndMiss.Core.Helpers;
using UROC.HitAndMiss.Core.Requests;


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
        [HttpGet("HitAndMissPlayRequest")]
        public async Task<ActionResult<HitAndMissPlayRequest>> GetHitAndMissPlayRequest(long gameID, double betAmount)
        {
            return await _gameModel.GetHitAndMissPlayRequest(gameID, betAmount);

        }
        [HttpGet("HitAndMissConfigurables")]
        public async Task<ActionResult<HitAndMissMathsResolve>> GetHitAndMissConfigurables(double HitWeight_Win, double HitWeight_Loose, [FromQuery(Name = "multiplier_combos")] int[] multiplierCombos)
        {
            return await _gameModel.GetHitAndMissConfigurables(HitWeight_Win, multiplierCombos);
        }

        //CREATE ANOHTER API ENDPOINT HERE THAT TAKES IN THE HitAndMissPlayRequest and returns a newly created HitAndMissResultResponse with the base data in for the game output.
        // THIS can be consumed by a front end as JSON, or reserviced to return anything we need to output.
    }
}
