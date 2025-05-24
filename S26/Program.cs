using System.Diagnostics;
using System.Collections.Concurrent;

namespace S26;
// Paralle.For
// AsParallel
// PLINQ
// Logging Singleton
// Factory

class Program
{
    static IEnumerable<int> GetNums()
    {
        System.Console.WriteLine("I'm here!!!");
        for (int i = 0; i >= 0; i++)
            yield return i;
    }

    static void Main5534(string[] args)
    {
        var nums = GetNums();
        foreach (var n in nums.Take(5))
            System.Console.WriteLine(n);
    }

    
    static void Main4545454(string[] args)
    {
        Stopwatch sw = Stopwatch.StartNew();
        var pnums =
            Enumerable.Range(2, MAX)
                    .GroupBy(n => Random.Shared.Next(1, 100))
                    .AsParallel()
                    .SelectMany(g => g.Where(n => IsPrime(n)));
        System.Console.WriteLine(pnums.Count());
        System.Console.WriteLine(sw.Elapsed.ToString());
    }

    static bool IsPrime(int n)
    {
        for (int i = 2; i <= Math.Sqrt(n); i++)
            if (n % i == 0)
                return false;
        return true;
    }
    const int MAX = 20_000_000;
    static void Main2(string[] args)
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

    static StreamWriter _writer = null;
    static object mylock = new object();
    static StreamWriter writer
    {
        get
        {
            if (_writer == null)
            {
                lock (mylock)
                {
                    if (_writer == null)
                        _writer = new StreamWriter("primes.txt");
                }
            }
            return _writer;

        }
    }

    static void Main(string[] args)
    {
        Stopwatch sw = Stopwatch.StartNew();
        int count = 0;
        // List<int> prime_nums = new List<int>();
        // ConcurrentBag<int> prime_nums = new ConcurrentBag<int>();
        Parallel.For(2, MAX, i =>
        {

            if (IsPrime(i))
            {
                Interlocked.Increment(ref count);
                lock (sw) writer.WriteLine($"{i}");
            }




            // lock(prime_nums)
            // prime_nums.Add(i);

            // Interlocked.Increment(ref count);
        });
        System.Console.WriteLine(sw.Elapsed.ToString());
        System.Console.WriteLine(count);
    }
}
