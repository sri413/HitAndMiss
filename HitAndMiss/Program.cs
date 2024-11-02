#region Assembly ClosedXML, Version=0.102.2.0, Culture=neutral, PublicKeyToken=fd1eb21b62ae805b
// C:\Users\abhay\.nuget\packages\closedxml\0.102.2\lib\netstandard2.1\ClosedXML.dll
// Decompiled with ICSharpCode.Decompiler 8.1.1.7464
#endregion

using System.Linq;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;
using System.Linq;
using DocumentFormat.OpenXml.Office.Word;
using System.Security.Principal;
using System.Diagnostics;
using Irony.Parsing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.Excel;
using System.Security.AccessControl;

public class HitAndMiss
{
    public string NumberOfSims;
    public string Gametype;
    private double GameID;
    private double Game_type;
    Random rand = new Random();
    List<int> multiplierValue = new List<int>() { 1, 2, 3, 5, 10, 25 };
    List<int> multiplierWeight = new List<int>() { 875000, 50000, 33333, 20833, 12500, 8333 };
    Dictionary<int, int> PerMultiplierWin = new Dictionary<int, int>();
    public int WinValue = 0;
    public int BetAmount = 1;
    int totalWin = 0;
    double RTP = 0;
    public string xlFilename = "main.xlsx";
    List<int> W_three = new List<int>();
    List<int> W_five = new List<int>();
    List<double> W_threeWL = new List<double>();
    List<double> W_fiveWL = new List<double>();
    List<int> M_three = new List<int>();
    List<int> M_five = new List<int>();
    List<List<int>> three_combo = new List<List<int>>();
    List<List<int>> five_combo = new List<List<int>>();

    public static void Main(string[] args)
    {
        HitAndMiss hnm = new HitAndMiss();

        hnm.readdata();
        hnm.CalculateSpin();
    }

    private void CalculateSpin()
    {
        Console.WriteLine("Enter number of simulations");
        NumberOfSims = Console.ReadLine();
        if (NumberOfSims != "")
        {
            GameID = double.Parse(NumberOfSims);

        }
        Console.WriteLine("Enter Type of game to play");
        Gametype = Console.ReadLine();
        //Game_type = double.Parse(Gametype);
        if (Gametype == "3")
        {
            for (int i = 0; i < GameID; i++)
            {
                RNGGenerator(3);
            }
        }
        else if (Gametype == "5")
        {
            for (int i = 0; i < GameID; i++)
            {
                RNGGenerator(5);
            }
        }
        else
        {
            for (int i = 0; i < GameID; i++)
            {
                RNGGenerator(1);
            }
        }
        RTP = (totalWin / (GameID * BetAmount));
        foreach (var i in PerMultiplierWin)
        {
            Console.WriteLine("Key = {0}, Value = {1}", i.Key, i.Value);
        }

        Console.WriteLine("RTP = {0}", RTP);

    }

    private void RNGGenerator(int g_type)
    {
        if (g_type == 3)
        {
            //int randomNumber = GenerateWeightedRandomNumber(null, W_threeWL);
            double randomNumber = rand.Next(0, (int)GameID);

            if (randomNumber >= (W_threeWL[0] * GameID))
            {
                SpinLooseCalculation(3);
            }
            else
            {
                SpinWinCalculation(3);
            }

        }
        else if (g_type == 5)
        {
            //int randomNumber = GenerateWeightedRandomNumber(null, W_fiveWL);
            double randomNumber = rand.Next(0, (int)GameID);

            if (randomNumber >= (W_threeWL[0] * GameID))
            {
                SpinLooseCalculation(5);
            }
            else
            {
                SpinWinCalculation(5);
            }
        }
        else
        {
            int randomNumber = rand.Next(0, (int)GameID);
            if (randomNumber <= 0.6 * GameID)
            {
                SpinWinCalculation(1);
            }
            else
            {
                SpinLooseCalculation(1);
            }
        }
    }

    private void SpinWinCalculation(int g_type)
    {

        if (g_type == 3)
        {
            CalculateWin(null, W_three, M_three);
        }
        else if (g_type == 5)
        {
            CalculateWin(null, W_five, M_five);
        }
        else if(g_type == 1)
        {
            //CalculateWin(multiplierValue, multiplierWeight, );
            int randomMultiplier = GenerateWeightedRandomNumber(multiplierValue, multiplierWeight);
            WinValue = randomMultiplier * BetAmount;
            totalWin += WinValue;
            if (!PerMultiplierWin.ContainsKey(WinValue))
                PerMultiplierWin.Add(WinValue, 1);
            else
                PerMultiplierWin[WinValue]++;
        }
    }

    private void CalculateWin(List<int> multiplierValues ,List<int> WeightCombo, List<int> W_ComboType )
    {
        int combo_type = GenerateWeightedRandomNumber(multiplierValues, WeightCombo);
        int multi = W_ComboType[combo_type];
        WinValue = multi * BetAmount;
        totalWin += WinValue;
        if (!PerMultiplierWin.ContainsKey(multi))
            PerMultiplierWin.Add(multi, 1);
        else
            PerMultiplierWin[multi]++;
    }

    private void SpinLooseCalculation(int g_type)
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
        for (int weightSumIdx = 0; weightSumIdx < weights.Count; weightSumIdx++)
        {
            weightSum += weights[weightSumIdx];
        }
        long r = rand.Next(0, weightSum);
        for (int i = 0; i < weights.Count; i++)
        {
            sum += weights[i];
            if (r < sum)
                return (values != null) ? values[i] : i;
        }
        Console.Write("All weights are 0");
        return -1;

    }

    private void readdata()
    {
        using (var workbook = new XLWorkbook(xlFilename))
        {
            //Processing data from excel sheet Three_turns
            var worksheet = workbook.Worksheet("Weight_three");

            //RangeExcel.Range range;
            var a = worksheet.Range("W_THREE");
            IXLRange reelRange = workbook.Range("W_THREE");


            int Row = 0;
            foreach (var row in a.RowsUsed())
            {


                foreach (var cell in row.CellsUsed())
                {
                    W_three.Add((int)(cell.Value));
                }
                Row++;
            }

            var worksheet1 = workbook.Worksheet("Weight_five");

            //RangeExcel.Range range;
            var b = worksheet1.Range("W_FIVE");
            //IXLRange reelRange = workbook.Range("W_THREE");


            Row = 0;
            foreach (var row in b.RowsUsed())
            {
                foreach (var cell in row.CellsUsed())
                {
                    W_five.Add((int)(cell.Value));
                }
                Row++;
            }
            var worksheet2 = workbook.Worksheet("Weight_three");

            //RangeExcel.Range range;
            var c = worksheet2.Range("THREE_WCOMBO");
            //IXLRange reelRange = workbook.Range("W_THREE");


            Row = 0;
            foreach (var row in c.RowsUsed())
            {
                three_combo.Add(new List<int>());
                foreach (var cell in row.CellsUsed())
                {
                    three_combo[Row].Add((int)(cell.Value));
                }
                Row++;
            }

            var worksheet5 = workbook.Worksheet("Three_Turns");

            //RangeExcel.Range range;
            var f = worksheet5.Range("FIVE_WCOMBO");
            //IXLRange reelRange = workbook.Range("W_THREE");


            Row = 0;
            foreach (var row in f.RowsUsed())
            {
                five_combo.Add(new List<int>());
                foreach (var cell in row.CellsUsed())
                {
                    five_combo[Row].Add((int)(cell.Value));
                }
                Row++;
            }


            var worksheet3 = workbook.Worksheet("Five_Turns");

            //RangeExcel.Range range;
            var d = worksheet3.Range("FIVE_WINLOSS");
            //IXLRange reelRange = workbook.Range("W_THREE");


            Row = 0;
            foreach (var row in d.RowsUsed())
            {
                foreach (var cell in row.CellsUsed())
                {
                    W_fiveWL.Add((double)(cell.Value));
                }
                Row++;
            }

            var worksheet4 = workbook.Worksheet("Three_Turns");

            //RangeExcel.Range range;
            var e = worksheet4.Range("THREE_WINLOSS");
            //IXLRange reelRange = workbook.Range("W_THREE");


            Row = 0;
            foreach (var row in e.RowsUsed())
            {
                foreach (var cell in row.CellsUsed())
                {
                    W_threeWL.Add((double)(cell.Value));
                }
                Row++;
            }

            var worksheet6 = workbook.Worksheet("Three_Turns");

            //RangeExcel.Range range;
            var g = worksheet6.Range("Multiplier_three");
            //IXLRange reelRange = workbook.Range("W_THREE");


            Row = 0;
            foreach (var row in g.RowsUsed())
            {
                foreach (var cell in row.CellsUsed())
                {
                    M_three.Add((int)(cell.Value));
                }
                Row++;
            }

            var worksheet7 = workbook.Worksheet("Five_Turns");

            //RangeExcel.Range range;
            var h = worksheet7.Range("Multiplier_five");
            //IXLRange reelRange = workbook.Range("W_THREE");


            Row = 0;
            foreach (var row in h.RowsUsed())
            {
                foreach (var cell in row.CellsUsed())
                {
                    M_five.Add((int)(cell.Value));
                }
                Row++;
            }

        }
    }
}