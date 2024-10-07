using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UROC.HitAndMiss.Core.Interfaces;
using UROC.HitAndMiss.Core.Responses;

namespace UROC.HitAndMiss.Core.Models
{
    public class HitAndMissModel : IHitAndMissModel
    {
        public async Task<HitAndMissGameResponse>  GetGameDetails(long gameId)
        {
            HitAndMissGameResponse response = new HitAndMissGameResponse();

            response = await HitAndMissGameResponse.GetGame_Test(gameId);

            return response;
        }
    }
}
