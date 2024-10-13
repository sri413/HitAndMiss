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
        public string Name { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Get Game details wrt game ids.
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns> Game Name and Decription specifying type of the game</returns>
        /// <exception cref="Exception">
        /// When <paramref name="gameId"/> doesn't match with list of game Ids. 
        /// </exception>
        internal async static Task<HitAndMissGameResponse> GetGameDetails(long gameId)
        {
            HitAndMissGameResponse response = new HitAndMissGameResponse();

            switch (gameId)
            {
                case 0:
                    {
                        response.Name = "Stars";
                        response.Description = "A megaways game";
                    };
                    break;
                case 1:
                    {
                        response.Name = "Sugar Rush";
                        response.Description = "Line Pay";
                    };
                    break;
                case 2:
                    {
                        response.Name = "Samurai Kings";
                        response.Description = "Tumbling";
                    };
                    break;
                case 3:
                    {
                        response.Name = "Eyes of the dragon";
                        response.Description = "Line Pay";
                    };
                    break;
                //case 4:
                //    {
                //        response.Name = "Stars";
                //        response.Description = "A megaways game";
                //    };
                //    break;
                default:
                    {
                        throw new Exception("Invalid Game details");
                    };
            }
            return response;
        }
        

    }
}