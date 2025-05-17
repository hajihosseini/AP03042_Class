public class Program2
{
    static object lockobj = new object();
    static void Do1(object obj)
    {
        try
        {
            System.ConsoleColor c = (System.ConsoleColor)obj;
            while (true)
            {
                lock (lockobj)
                {
                    System.ConsoleColor pc = Console.ForegroundColor;
                    Console.ForegroundColor = c;
                    System.Console.WriteLine(c.ToString());
                    Console.ForegroundColor = pc;
                }
                    Thread.Sleep(1000);
            }
        }
        catch { }
        {
            System.Environment.Exit(-1);
        }
    }

    public static void Main(string[] args)
    {
        Thread t = new Thread(Do1);
        t.Start(System.ConsoleColor.Yellow);
        t.IsBackground = true;
        Thread t2 = new Thread(Do1);
        t2.Start(System.ConsoleColor.Cyan);
        Thread.Sleep(1);
        while (true)
        {
            lock (lockobj)
            {
                System.Console.WriteLine("Main");
            }
                Thread.Sleep(1000);
        }
    }
}