using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UROC.HitAndMiss.Core.Responses;

namespace UROC.HitAndMiss.Core.Interfaces
{
    public interface IHitAndMissModel
    {
        Task<HitAndMissGameResponse> GetGameDetails(long gameId);
    }
}
