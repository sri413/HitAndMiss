using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UROC.HitAndMiss.Core.Requests
{
    public class HitAndMissPlayRequest
    {
        public long gameId { get; set; }
        public decimal betAmount { get; set; }
    }
}
