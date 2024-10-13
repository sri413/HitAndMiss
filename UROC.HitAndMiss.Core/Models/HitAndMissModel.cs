using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UROC.HitAndMiss.Core.Helpers;
using UROC.HitAndMiss.Core.Interfaces;
using UROC.HitAndMiss.Core.Requests;
using UROC.HitAndMiss.Core.Responses;

namespace UROC.HitAndMiss.Core.Models
{
    public class HitAndMissModel : IHitAndMissModel
    {
        public async Task<HitAndMissGameResponse> GetGameDetails(long gameId)
        {
            HitAndMissGameResponse response = new HitAndMissGameResponse();

            response = await HitAndMissGameResponse.GetGameDetails(gameId);

            return response;
        }
        public async Task<HitAndMissPlayRequest> GetHitAndMissPlayRequest(long gameID, double betAmount)
        {
            HitAndMissPlayRequest req = new HitAndMissPlayRequest();

            req = await HitAndMissPlayRequest.GetHitAndMissRequest(gameID, betAmount);

            return req;
        }
        public async Task<HitAndMissMathsResolve> GetHitAndMissConfigurables(double HitWeight_Win, int[] multiplierCombos)
        {
            HitAndMissMathsResolve resolveMathsHitAndMiss = new HitAndMissMathsResolve();
            resolveMathsHitAndMiss = await HitAndMissMathsResolve.ResolveMaths(HitWeight_Win, multiplierCombos);
            return resolveMathsHitAndMiss;
        }
    }
}
