using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UROC.HitAndMiss.Core.Responses;
using UROC.HitAndMiss.Core.DTOs;
using System.Text.Json.Serialization;

namespace UROC.HitAndMiss.Core.Helpers
{
    public class HitAndMissMathsResolve
    {

        //public string NumberOfSims;
        //private double GameID;
        //List<int> multiplierValue = new List<int>() { 1, 2, 3, 5, 10, 25 };


        [JsonIgnore]
        Random rand = new Random();
        [JsonIgnore]
        List<int> multiplierWeight = new List<int>() { 875000, 50000, 33333, 20833, 12500, 8333 };
        [JsonIgnore]
        Dictionary<int, int> PerMultiplierWin = new Dictionary<int, int>();

        public int WinValue;
        private static int WinAmount;
        private static double BetAmount;
        public static int TotalnumberOfSpins;
        private double RTP;

        //Return as response
        public int totalWin { get; set; }
        public int thisWin { get; set; }
        public double totalBetAmount { get; set; }
        public double thisBetAmount { get; set; }


        public async static Task<HitAndMissMathsResolve> ResolveMaths(double hitWeight_Win, int[] multiplierCombos)
        {
            HitAndMissDTO.GetDTOInstance.HitWeight_Win = hitWeight_Win;
            HitAndMissDTO.GetDTOInstance.MultiplierCombos = multiplierCombos;


            HitAndMissMathsResolve resolve = new HitAndMissMathsResolve();
            resolve.CalculateSpin();

            return resolve;
        }

        private void CalculateSpin()
        {
            TotalnumberOfSpins++;
            //Console.WriteLine("Enter number of simulations");
            //NumberOfSims = Console.ReadLine();
            //if (NumberOfSims != "")
            //{
            //    GameID = double.Parse(NumberOfSims);

            //}
            //for (int i = 0; i < 10000000; i++)
            //{
            //    RNGGenerator();
            //}
            RNGGenerator();
            BetAmount += HitAndMissDTO.GetDTOInstance.BetAmount;
            totalBetAmount = BetAmount;
            thisBetAmount = HitAndMissDTO.GetDTOInstance.BetAmount;
            //foreach (var i in PerMultiplierWin)
            //{
            //    Console.WriteLine("Key = {0}, Value = {1}", i.Key, i.Value);
            //}
        }

        private void RNGGenerator()
        {
            int randomNumber = rand.Next(0, 100);
            if (randomNumber <= HitAndMissDTO.GetDTOInstance.HitWeight_Win)
            {
                SpinWinCalculation();
            }
            else
            {
                SpinLooseCalculation();
            }
        }

        private void SpinWinCalculation()
        {
            int randomMultiplier = GenerateWeightedRandomNumber(HitAndMissDTO.GetDTOInstance.MultiplierCombos, multiplierWeight, 0);
            WinValue = randomMultiplier * (int)HitAndMissDTO.GetDTOInstance.BetAmount;
            thisWin = WinValue;
            WinAmount += WinValue;
            totalWin = WinAmount;
            //if (!PerMultiplierWin.ContainsKey(WinValue))
            //    PerMultiplierWin.Add(WinValue, 1);
            //else
            //    PerMultiplierWin[WinValue]++;


        }
        private void SpinLooseCalculation()
        {
            totalWin = WinAmount;
            //if (!PerMultiplierWin.ContainsKey(WinValue))
            //    PerMultiplierWin.Add(WinValue, 1);
            //else
            //    PerMultiplierWin[WinValue]++;
        }

        protected virtual int GenerateWeightedRandomNumber(int[] comboValues, List<int> weights, int fixedValue = 0)
        {
            long sum = 0;
            int weightSum = 0;
            for (int weightSumIdx = 0; weightSumIdx < weights.Count; weightSumIdx++)
            {
                weightSum += weights[weightSumIdx];
            }
            long r = rand.Next(0, weightSum);
            for (int i = 0; i < weights.Count; i++)
            {
                sum += weights[i];
                if (r < sum)
                    return (comboValues != null) ? comboValues[i] : i;
            }
            Console.Write("All weights are 0");
            return -1;

        }
    }
}
