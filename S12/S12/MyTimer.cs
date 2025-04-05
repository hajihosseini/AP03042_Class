using System.Diagnostics;


public class MyTimer : IDisposable
{
    public string name;
    private Stopwatch sw = new Stopwatch();

    public MyTimer(string nm)
    {
        name = nm;

        sw.Start();
    }

    public void Dispose()
    {
        sw.Stop();
        System.Console.WriteLine($"{this.name},{this.sw.Elapsed.ToString()}");
    }

    public void printSW()
    {
        System.Console.WriteLine($"{this.name}");
    }
}