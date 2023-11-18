using System.Collections.Concurrent;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    { 
        Sample(100000);
        Sample(1000000);
        Sample(10000000);
        Console.WriteLine($"* Время измеряется в тиках процессора");
        Console.ReadKey();
    }
    private static void Sample(int length)
    {
        var arr = new int[length]; 
        for (int i = 0; i < length; i++)
        {
            arr[i] = i;
        }
        var list = new List<int>(arr);
        var stopwatch = new Stopwatch();
        Console.WriteLine($"Число элементов: {length}");
        stopwatch.Start();
        var sum1 = Sum1(arr);
        stopwatch.Stop();
        Console.WriteLine($"Обычное Время: {stopwatch.ElapsedTicks}");
        stopwatch.Reset();
        stopwatch.Start();
        var sum2 = Sum2(list);
        stopwatch.Stop();
        Console.WriteLine($"Thread  Время: {stopwatch.ElapsedTicks}");
        stopwatch.Reset();
        stopwatch.Start();
        var sum3 = Sum3(arr);
        stopwatch.Stop();
        Console.WriteLine($"LINQ    Время: {stopwatch.ElapsedTicks}");
        stopwatch.Reset();
    }



    private static long Sum1(int[] input)
    {
        long sum = 0;
        foreach (var item in input)
        {
            sum += item;
        }
        return sum;

    } 
    private static long Sum2(List<int> input)
    {
        long sum = 0;
        Parallel.ForEach(input, value =>
        {
            sum += value;
        });
        return sum;
    }
    private static long Sum3(int[] input)
    {
        long sum = 0;
        Array.ForEach(input, delegate (int i) { sum += i; });
        return sum;
    }


}