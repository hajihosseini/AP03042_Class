using System.Diagnostics;

namespace S26;
// Paralle.For
// AsParallel
// PLINQ
// Logging Singleton
// Factory

class Program
{
    static bool  IsPrime(int n)
    {
        for (int i = 2; i <= Math.Sqrt(n); i++)
            if (n % i == 0)
                return false;
        return true;
    }
    const int MAX = 20_000_000;
    static void Main(string[] args)
    {
        Stopwatch sw = Stopwatch.StartNew();
        int count = 0;
        Enumerable.Range(2, MAX)
                  .ToList()
                  .ForEach(i =>
                    {
                        if (IsPrime(i))
                            count++;
                    });
        System.Console.WriteLine(sw.Elapsed.ToString());
        System.Console.WriteLine(count);
    }

    static void Main2(string[] args)
    {
        Stopwatch sw = Stopwatch.StartNew();
        int count = 0;
        var pflr = Parallel.For(2, MAX, i =>
        {
            if (IsPrime(i))
                Interlocked.Increment(ref count);
        });
        System.Console.WriteLine(sw.Elapsed.ToString());
        System.Console.WriteLine(count);
    }
}
