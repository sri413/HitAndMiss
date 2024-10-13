using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UROC.HitAndMiss.Core.Responses;
using UROC.HitAndMiss.Core.Helpers;
using UROC.HitAndMiss.Core.Requests;

namespace UROC.HitAndMiss.Core.Interfaces
{
    public interface IHitAndMissModel
    {
        Task<HitAndMissGameResponse> GetGameDetails(long gameId);
        Task<HitAndMissPlayRequest> GetHitAndMissPlayRequest(long gameID, double betAmount);
        Task<HitAndMissMathsResolve> GetHitAndMissConfigurables(double HitWeight_Win, int[] multiplierCombos);
    }
}