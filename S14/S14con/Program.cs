namespace S14con;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Vector<int> v = new Vector<int>(3,4);
        bool b = v.Equals("3,4");
        System.Console.WriteLine(b);

        // AngleVector
        // Vector v;
        // v.X = 5;
        // int w = v.X;
    }
}
