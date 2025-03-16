using System.Diagnostics;
using System.Text;

namespace s10con;

class Program
{

    static void Main(string[] args)
    {
        Student s = new Student("ali");
        s.set_Name("Ali");
        s.LastName = "Mohammadi";
        System.Console.WriteLine(s.FullName);
        using (StreamReader reader = new StreamReader(""))
        using (StreamWriter writer = new StreamWriter(""))
        {
        }

    }

    static void MainTT(string[] args)
    {
        if (args.Length == 0)
        {
            using Student s = new Student("Hadi");
            System.Console.WriteLine("in block");
            Student ss = new Student("madi");
            ss = null;            
        }
        GC.Collect();
        Thread.Sleep(1000);
        System.Console.WriteLine("end main");
    }


    static void MainSSS(string[] args)
    {
        using (MyTimer mt = new MyTimer("string add 75000"))
        {
            string s = "";
            for(int i=0; i<75000; i++)
            {
                s += i.ToString() + ',';
            }
            System.Console.WriteLine(s.Length);
        }
    }

    #region Hide

    static void Main6(string[] args)
    {
        using StreamWriter writer = new StreamWriter("a.txt");
        writer.WriteLine("test");
    }

    static void Main5(string[] args)
    {
        StringBuilder sb = new StringBuilder();
        using (StringWriter sw = new StringWriter(sb))
        {
            sw.WriteLine("test");            
        }
        System.Console.WriteLine(sb.ToString());
    }

    static void Main4(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        StringBuilder sb = new StringBuilder(500000);
        for(int i=0; i<75000; i++)
        {
            sb.Append(i.ToString());
            sb.Append(',');
        }
        System.Console.WriteLine(sb.ToString().Length);
        sw.Stop();
        System.Console.WriteLine(sw.Elapsed.ToString());

    }

    static void Main3(string[] args)
    {
        //StringWriter writer = new StreamWriter();
        Stopwatch sw = new Stopwatch();
        sw.Start();
        string s = "";
        for(int i=0; i<75000; i++)
        {
            s += i.ToString() + ',';
        }
        System.Console.WriteLine(s.Length);
        sw.Stop();
        System.Console.WriteLine(sw.Elapsed.ToString());
    }

    static void Main2(string[] args)
    {
        // string[] lines = File.ReadAllLines("test.txt");
        // File.WriteAllLines("test2.txt", lines);
        if (args.Length > 2 || args.Length<1)
        {
            System.Console.WriteLine("Usage: program.exe <intput_file> [out_file]");            
            return;
        }
        string outFile = null;
        if (args.Length>1)
        {
            outFile = args[1];
        }
        int linecount = 0;
        int charCount = 0;
        int wordCount = 0;

        using (StreamReader reader = new StreamReader(args[0]))
        {
            string line;
            while ( null != (line = reader.ReadLine()))
            {
                linecount++;
                charCount+= line.Length;
                foreach(string s in line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    wordCount++;
                }
            }
        }
        string outstr = $"{linecount} {wordCount} {charCount}";
        if (outFile != null)
        {
            using (StreamWriter writer = new StreamWriter(outFile))
            {
                writer.WriteLine(outstr);
            }
        }
        else
            System.Console.WriteLine(outstr);        

    }
    #endregion
}
