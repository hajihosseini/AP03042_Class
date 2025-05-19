using System.ComponentModel;
using System.Text.RegularExpressions;

namespace S25;

class Program
{
    static double dowork(object obj)
    {
        int n = (int)obj;
        System.Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} => {n}");
        double d = 1;
        for (int i = 1; i < n; i++)
            d = (d * i) % int.MaxValue;
        return d;
    }

    static void Main(string[] args)
    {
        var tasks = new Task<double>[19];
        double sum = 0;
        for (int i = 1; i < 20; i++)
        {
            tasks[i - 1] = Task<double>.Factory.StartNew(dowork, i * 10_000_000);
        }
        Task.WaitAll(tasks);

        foreach (var t in tasks)
            sum += t.Result;
        System.Console.WriteLine(sum);
    }

    static void Main3(string[] args)
    {
        var tasks = new Task<double>[19];
        double sum = 0;
        for (int i = 1; i < 20; i++)
        {
            tasks[i - 1] = Task<double>.Factory.StartNew(dowork, i * 10_000_000);
        }
        Task.WaitAll(tasks);

        foreach (var t in tasks)
            sum += t.Result;
        System.Console.WriteLine(sum);
    }

    static void Main2(string[] args)
    {
        var t = Task.Run(() => dowork(10_000_000));
        while (true)
        {
            if (t.Status == TaskStatus.RanToCompletion)
                break;
            System.Console.WriteLine("Waiting");
            Task td = Task.Delay(10);
            td.Wait();
        }
        System.Console.WriteLine(t.Result);
    }
}
