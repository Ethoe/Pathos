using System;
using System.Linq;
using System.Collections.Generic;

public class WeightedChanceParam
{
    public Action Func { get; }
    public double Ratio { get; }

    public WeightedChanceParam(Action func, double ratio)
    {
        Func = func;
        Ratio = ratio;
    }
}

/// WeightedChanceExecutor weightedChanceExecutor = new WeightedChanceExecutor(
///  new WeightedChanceParam(() =>
///   {
///    Console.Out.WriteLine("A");
///   }, 25), //25% chance (since 25 + 25 + 50 = 100)
///  new WeightedChanceParam(() =>
///   {
///    Console.Out.WriteLine("B");
///   }, 50), //50% chance
///  new WeightedChanceParam(() =>
///   {
///    Console.Out.WriteLine("C");
///   }, 25) //25% chance
///  );
/// 
/// 25% chance of writing "A", 25% chance of writing "C", 50% chance of writing "B"        
/// weightedChanceExecutor.Execute();
public class WeightedChanceExecutor
{
    public List<WeightedChanceParam> Parameters { get; }
    private Random r;

    public double RatioSum
    {
        get { return Parameters.Sum(p => p.Ratio); }
    }

    public WeightedChanceExecutor(params WeightedChanceParam[] parameters)
    {
        Parameters = parameters.ToList<WeightedChanceParam>();
        r = new Random();
    }

    public void AddChance(WeightedChanceParam param)
    {
        Parameters.Add(param);
    }

    public void Execute()
    {
        double numericValue = r.NextDouble() * RatioSum;

        foreach (var parameter in Parameters)
        {
            numericValue -= parameter.Ratio;

            if (!(numericValue <= 0))
                continue;

            parameter.Func();
            return;
        }

    }
}