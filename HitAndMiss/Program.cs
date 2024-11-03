using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using Irony.Parsing;

class HitAndMiss
{
    private double totalWin;
    private double BetAmount = 1.0; // Assume some default value for BetAmount
    private double GameID;
    private string NumberOfSims;
    private string Gametype;
    private double RTP;
    private double WinValue;

    private Dictionary<double, int> PerMultiplierWin = new Dictionary<double, int>();
    private Random rand = new Random();

    List<int> multiplierValue = new List<int>() { 1, 2, 3, 5, 10, 25 };
    List<int> multiplierWeight = new List<int>() { 875000, 50000, 33333, 20833, 12500, 8333 };


    public static void Main(string[] args)
    { 
       
        ExcelParser.GetXlParserInstance.ReadData();

        HitAndMiss hnm = new HitAndMiss();
        hnm.CalculateSpin();
    }

    private void CalculateSpin()
    {
        Console.WriteLine("Enter number of simulations");
        NumberOfSims = Console.ReadLine();
        if (double.TryParse(NumberOfSims, out GameID))
        {
            Console.WriteLine("Enter Type of game to play");
            Gametype = Console.ReadLine();
            for (int i = 0; i < GameID; i++)
            {
                switch (Gametype)
                {
                    case "3":
                        RNGGenerator(3);
                        break;
                    case "5":
                        RNGGenerator(5);
                        break;
                    default:
                        RNGGenerator(1);
                        break;
                }
            }

            RTP = totalWin / (GameID * BetAmount);
            foreach (var i in PerMultiplierWin)
            {
                Console.WriteLine("Key = {0}, Value = {1}", i.Key, i.Value);
            }
            Console.WriteLine("RTP = {0}", RTP);
        }
        else
        {
            Console.WriteLine("Invalid input for number of simulations.");
        }
    }

    private void RNGGenerator(int gType)
    {
        int randomNumber = rand.Next(0, (int)GameID);
        double threshold = gType == 3 ? ExcelParser.GetXlParserInstance.W_threeWL[0] * GameID : ExcelParser.GetXlParserInstance.W_fiveWL[0] * GameID;

        if (gType == 1)
        {
            if (randomNumber <= 0.6 * GameID)
            {
                SpinWinCalculation(gType);
            }
            else
            {
                SpinLooseCalculation(gType);
            }
        }
        else
        {
            if (randomNumber >= threshold)
            {
                SpinLooseCalculation(gType);
            }
            else
            {
                SpinWinCalculation(gType);
            }
        }
    }

    private void SpinWinCalculation(int gType)
    {
        if (gType == 3)
        {
            CalculateWin(null, ExcelParser.GetXlParserInstance.W_three, ExcelParser.GetXlParserInstance.M_three);
        }
        else if (gType == 5)
        {
            CalculateWin(null, ExcelParser.GetXlParserInstance.W_five, ExcelParser.GetXlParserInstance.M_five);
        }
        else if (gType == 1)
        {
            int randomMultiplier = GenerateWeightedRandomNumber(multiplierValue, multiplierWeight);
            WinValue = randomMultiplier * BetAmount;
            totalWin += WinValue;
            if (!PerMultiplierWin.ContainsKey(WinValue))
                PerMultiplierWin.Add(WinValue, 1);
            else
                PerMultiplierWin[WinValue]++;
        }
    }

    private void CalculateWin(List<int> multiplierValues, List<int> WeightCombo, List<int> W_ComboType)
    {
        int comboType = GenerateWeightedRandomNumber(multiplierValues, WeightCombo);
        int multi = W_ComboType[comboType];
        WinValue = multi * BetAmount;
        totalWin += WinValue;
        if (!PerMultiplierWin.ContainsKey(multi))
            PerMultiplierWin.Add(multi, 1);
        else
            PerMultiplierWin[multi]++;
    }

    private void SpinLooseCalculation(int gType)
    {
        WinValue = 0;
        if (!PerMultiplierWin.ContainsKey(WinValue))
            PerMultiplierWin.Add(WinValue, 1);
        else
            PerMultiplierWin[WinValue]++;
    }

    protected virtual int GenerateWeightedRandomNumber(List<int> values, List<int> weights)
    {
        long sum = 0;
        int weightSum = 0;
        for (int i = 0; i < weights.Count; i++)
        {
            weightSum += weights[i];
        }
        long r = rand.Next(0, weightSum);
        for (int i = 0; i < weights.Count; i++)
        {
            sum += weights[i];
            if (r < sum)
                return values != null ? values[i] : i;
        }
        Console.WriteLine("All weights are 0");
        return -1;
    }

}