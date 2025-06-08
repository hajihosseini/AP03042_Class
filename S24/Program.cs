using System.Diagnostics;

namespace S24;

class Program
{
    static double CPUIntensive()
    {
        int n = 15;
        double d = n;
        for (int i = 0; i < 100_000_000; i++)
            d = d * Math.Sqrt(n) % n;
        return d;
    }


    static async Task<double> CPUIntensiveAsync(int n)
    {
        // int n = 15;
        double result = await Task.Run(() =>
        {
            double d = n;
            for (int i = 0; i < 100_000_000; i++)
                d = d * Math.Sqrt(n) % n;
            return d;
        });
        return result;
    }

    static void Main(string[] args)
    {
        CPUIntensiveAsync(15).Start();
        
        Stopwatch sw = Stopwatch.StartNew();
        double sum = 0;
        for (int i = 0; i < 20; i++)
            sum += CPUIntensive();
        sw.Stop();
        System.Console.WriteLine(sw.Elapsed.ToString());
        System.Console.WriteLine(sum);

    }


    static void Main2(string[] args)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Task<double>[] tasks = new Task<double>[20];
        for (int i = 0; i < 20; i++)
        {
            tasks[i] = new Task<double>(CPUIntensive);
            tasks[i].Start();
        }
        Task.WaitAll(tasks);
        double sum = 0;
        for (int i = 0; i < 20; i++)
            sum += tasks[i].Result;

        System.Console.WriteLine(sum);
        sw.Stop();
        System.Console.WriteLine(sw.Elapsed.ToString());
    }
}
