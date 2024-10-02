using MathNet.Numerics.Random;
using System.Linq;
public class HitAndMiss
{
    public string NumberOfSims;
    private double GameID;
    Random rand = new Random();
    List<int> multiplierValue = new List<int>() { 1, 2, 3, 5, 10, 25 };
    List<int> multiplierWeight = new List<int>() { 875000, 50000, 33333, 20833, 12500, 8333 };
    Dictionary<int, int> PerMultiplierWin = new Dictionary<int, int>();
    public int WinValue = 0;
    public int BetAmount = 10;
    int totalWin = 0;
    double RTP = 0;


    public static void Main(string[] args)
    {
        HitAndMiss hnm = new HitAndMiss();
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
        for (int i = 0; i < GameID; i++)
        {
            RNGGenerator();
        }
        RTP = (totalWin / (GameID * BetAmount));
        foreach (var i in PerMultiplierWin)
        {
            Console.WriteLine("Key = {0}, Value = {1}", i.Key, i.Value);
        }
    }

    private void RNGGenerator()
    {
        int randomNumber = rand.Next(0, 100);
        if (randomNumber <= 60)
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
        int randomMultiplier = GenerateWeightedRandomNumber(multiplierValue, multiplierWeight, 0);
        WinValue = randomMultiplier * BetAmount;
        totalWin += WinValue;
        if (!PerMultiplierWin.ContainsKey(WinValue))
            PerMultiplierWin.Add(WinValue, 1);
        else
            PerMultiplierWin[WinValue]++;
        
        
    }
    private void SpinLooseCalculation()
    {
        WinValue = 0;
        if (!PerMultiplierWin.ContainsKey(WinValue))
            PerMultiplierWin.Add(WinValue, 1);
        else
            PerMultiplierWin[WinValue]++;
    }

    protected virtual int GenerateWeightedRandomNumber(List<int> values, List<int> weights, int fixedValue = 0)
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
}

