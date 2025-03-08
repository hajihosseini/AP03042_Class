namespace S8con;

public class Program
{
    public static void reverse(string s, out string srev)
    {
        srev = "";
        foreach(char c in s)
            srev = c + srev;
    }
    public static int add(int a, int b)
    {
        return a+b;
    }

    public static void prepend_user(string s, out string sout)
    {
        sout = "user:"+s;
    }

    
    static void Main(string[] args)
    {
        System.Console.WriteLine(Student.StudentCount);
        Student s1 = new Student("zoha", 1232);
        System.Console.WriteLine(Student.StudentCount);
        Student s2 = new Student("ali", 4343);
        System.Console.WriteLine(Student.StudentCount);
        System.Console.WriteLine(s1.name);
    }
    static void Main2(string[] args)
    {
        string name = "fateme";
        string srev;
        reverse(name, out srev);
        System.Console.WriteLine(srev);
    }

    static void Main1(string[] args)
    {
        int sum = 0;
        while(true)
        {
            System.Console.WriteLine("Nums?");
            string s = Console.ReadLine();
            if(string.IsNullOrEmpty(s))
                break;

            int n;
            if(! int.TryParse(s,out n))
            {
                System.Console.WriteLine("Wrong! Try again");
                continue;
            }

            //sum += int.Parse(s);
            string sout;
            prepend_user(s, out sout);
            System.Console.WriteLine(sout);
            sum += n;
        }

        System.Console.WriteLine(sum);

        // for(int i=0;i<args.Length;i++)
        // {
        //     Console.WriteLine($"args{i}:{args[i]}");
        // }
        // Console.WriteLine("Hello, World!");
    }
}
