using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UROC.HitAndMiss.Core.DTOs;

namespace UROC.HitAndMiss.Core.Responses
{
    public class HitAndMissGameResponse
    {
        public HitAndMissDTO Game { get; set; }

        /// <summary>
        /// Todo => This should be eventually driven from a data model . For now hard code this from a static return...
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal async static Task<HitAndMissGameResponse> GetGame_Test(long gameId)
        {
            throw new NotImplementedException();
        }
    }
}
