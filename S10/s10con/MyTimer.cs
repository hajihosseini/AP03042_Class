using System.Diagnostics;

public class MyTimer: IDisposable
{
    private string Name;
    private Stopwatch sw;
    public MyTimer(string name)
    {
        this.Name = name;
        // this.sw = new Stopwatch();
        // this.sw.Start();
        this.sw = Stopwatch.StartNew();
    }

    public void Dispose()
    {
        this.sw.Stop();
        System.Console.WriteLine($"{this.Name} {this.sw.Elapsed.ToString()}");
    }
}