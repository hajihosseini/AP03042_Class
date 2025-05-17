namespace S24;

class Program
{
    static double CPUIntensive()
    {
        int n = 15;
        double d = n;
        for (int i = 0; i < 100_000; i++)
            d = d * Math.Sqrt(n) % n;
        return d;
    }

    static void Main(string[] args)
    {
        Task<double> t = new Task<double>()
    }
}
