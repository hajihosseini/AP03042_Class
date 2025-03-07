namespace S8con;

public class email
{
    public string user;
    string[] domains;
}

public class MyProgram
{
    public static int add(int a, int b)
    {
        return a+b;
    }

    private static void prepend_user(string s, out string sout)
    {
        sout = "user:" + s;
    }

    public static int Reverse(string s, out string srev)
    {
        srev = "";
        foreach(char ch in s)
            srev = ch + srev;
        return s.Length;
    }

    public static string ReverseWords(string sent)
    {
        string[] words = sent.Split();
        string[] wordsrev = new string[words.Length];
        for(int i=words.Length-1; i>=0; i--)
        {
            wordsrev[words.Length-1-i] += words[i];
        }
        return string.Join(" ", wordsrev);
    }

    public static void decompose_email(string email)
    {
        // test@comp.iust.ac.ir
        // user = test
        // domains = {"comp", "iust", "ac", "ir" }
        // URL
        // Is Valid email
        // Is Valid URL
        

    }

    static void Main(string[] args)
    {
        string s = "123456789";
        string sr;
        Reverse(s, out sr);
        System.Console.WriteLine(sr);

        string sent = "I like a book";
        System.Console.WriteLine(ReverseWords(sent));
    }

    static void Main4(string[] args)
    {
        int sum = 0;
        while(true)
        {
            Console.Write("Num? ");
            string s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s)) // s == null || s == "" || s == " "
                break;
            // int n = int.Parse(s);
            int n;
            //double.Parse, double.TryParse
            if (! int.TryParse(s, out n))
            {
                System.Console.WriteLine("Not a number. Try again.");
                continue;
            }
            string sout;
            prepend_user(s, out sout);
            System.Console.WriteLine(sout);
            sum += n;
        }
        System.Console.WriteLine(sum);
    }

    static void Main1(string[] args)
    {
        Student s = new Student("ali", 2342);
        s.add_grade(19.5);
        System.Console.WriteLine(Student.GetTotalStudentCount());
        Student s2 = new Student("zari", 3243);
        System.Console.WriteLine(Student.GetTotalStudentCount());
    }
}
