using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UROC.HitAndMiss.Core.DTOs;
using UROC.HitAndMiss.Core.Helpers;

namespace UROC.HitAndMiss.Core.Requests
{
    public class HitAndMissPlayRequest
    {
        public long GameID { get; set; }
        public double BetAmount { get; set; }

        public static HitAndMissDTO Game;
        /// <summary>
        /// Get game request and play the game as per request wrt game ID;
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="betAmount"></param>
        /// <returns>GameId and stake value</returns>
        /// <exception cref="Exception">
        /// When <paramref name="betAmount"/> is < 0.2 and > 10.0.
        /// </exception>
        public async static Task<HitAndMissPlayRequest> GetHitAndMissRequest(long gameID, double betAmount)
        {
            if (betAmount < 0.2 || betAmount > 10.0) 
            {
                throw new Exception("Invalid Bet Amount");
            }
            else 
            {
                HitAndMissPlayRequest response = new()
                {
                    BetAmount = betAmount,
                    GameID = gameID

                };
                HitAndMissDTO.GetDTOInstance.GameId = gameID;
                HitAndMissDTO.GetDTOInstance.BetAmount = betAmount; ;
                return response;

                
            }
        }
    }
}
