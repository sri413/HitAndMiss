using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UROC.HitAndMiss.Core.DTOs
{
    public class HitAndMissDTO
    {
        private HitAndMissDTO() { }

        static HitAndMissDTO hitAndMissDTOInstance = null;
        public static HitAndMissDTO GetDTOInstance
        {
            get
            {
                if (hitAndMissDTOInstance == null)
                {
                    hitAndMissDTOInstance = new HitAndMissDTO();
                }
                return hitAndMissDTOInstance;
            }
        }

        public long GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public double HitWeight_Win { get; set; }
        public int[] MultiplierCombos { get; set; }
        //Etc...

        public double WinAmount { get; set; }
        public double BetAmount { get; set; }

        public double RTP { get; set; }
        public bool Win { get; set; }
    }
}
