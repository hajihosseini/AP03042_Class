using System.Diagnostics;

namespace S23;

class Program
{
    static void Main(string[] args)
    {
        AutoResetEvent ase = new AutoResetEvent(false);
        int counter = 1_000;
        var s = Stopwatch.StartNew();
        for(int i=0; i<1_000; i++)
            new Thread(() => {
                //....
                CPUIntensive(i + 10);
                Interlocked.Decrement(ref counter);
                if (counter == 0)
                    ase.Set();

            }).Start();

        ase.WaitOne();
        s.Stop();
        System.Console.WriteLine(s.Elapsed.ToString());
    }

    static double CPUIntensive(int n)
    {
        double d = n;
        for (int i = 0; i < 100_000; i++)
            d = d * Math.Sqrt(n) % n;
        return d;
    }
    static void Main55(string[] args)
    {
        AutoResetEvent ase = new AutoResetEvent(false);
        int counter = 1_000;
        var s = Stopwatch.StartNew();
        for (int i = 0; i < 1_000; i++)
            ThreadPool.QueueUserWorkItem(obj =>
            {
                //....
                CPUIntensive(i + 10);
                Interlocked.Decrement(ref counter);
                if (counter == 0)
                    ase.Set();

            });

        ase.WaitOne();
        s.Stop();
        System.Console.WriteLine(s.Elapsed.ToString());
    }





    const int COUNT = 10_000_000;
    static int counter = 0;
    static void Main88(string[] args)
    {
        object mylock = new object();

        Thread t1 = new Thread(() => {
            for(int i=0; i<COUNT; i++)
                // lock(mylock)
                Interlocked.Increment(ref counter);
                    // counter++;
        });
        Thread t2 = new Thread(() => {
            for(int i=0; i<COUNT; i++)
                // lock(mylock)
                Interlocked.Decrement(ref counter);
                    // counter--;
        });
        Stopwatch s = Stopwatch.StartNew();
        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();
        s.Stop();
        System.Console.WriteLine(s.Elapsed.ToString());

        System.Console.WriteLine(counter);

    }
}
