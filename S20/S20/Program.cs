namespace S20;

public class MyTuple<T1,T2>
{
    public T1 Item1;
    public T2 Item2;

    public MyTuple(T1 i, T2 j)
    {
        this.Item1 = i;
        this.Item2 = j;
    }
}

class Program
{
    public static (int, int , double) AnalyzeList(List<int> numbers)
    {
        if(numbers == null || numbers.Count==0)
            throw new Exception("List must not be empty!");
        
        int min = numbers.Min();
        int max = numbers.Max();
        double avg = numbers.Average();
        avg = Math.Round(avg,2);

        return (min, max , avg);
    }

    static void Main(string[] args)
    {
        Tuple<string,int> t1 = new Tuple<string, int>("Ali",12);
        System.Console.WriteLine($"{t1.Item1}, {t1.Item2}");

        var t2 = new Tuple<string, int , int>("zoha",12, 1232);
        System.Console.WriteLine($"{t2.Item1}, {t2.Item2},{t2.Item3}");

        var t3 = Tuple.Create<string, int>("hoda", 123);
        System.Console.WriteLine($"{t3.Item1}, {t3.Item2}");

        MyTuple<string,int> t4 = new MyTuple<string, int>("Reza", 13);
        System.Console.WriteLine($"{t4.Item1},{t4.Item2}");

        ValueTuple<string,int> t5 = new ValueTuple<string,int>("hosna", 34);
        System.Console.WriteLine($"{t5.Item1},{t5.Item2}");

        List<int> nums = new List<int>{1,2,3,6,8,4,5};
        var result = AnalyzeList(nums);
        System.Console.WriteLine(result);





        

    }

    static void Main2(string[] args)
    {
        string name = "computer";
        string name2 =  name.TitleCase();
        
        string text = "sfhdfg1234ff971!";
        System.Console.WriteLine(text.CountDigit());
        System.Console.WriteLine(name2);
    }


    static void Main1(string[] args)
    {
        int x = 5;
        double y = 6.8;

        //y = x;      //implicit
        //x = (int)y; //explicit

        ComplexNumber c1 = new ComplexNumber(2,6);
        ComplexNumber c2 = new ComplexNumber(1,9);

        y = c1;  //implicit
        c2 = y;

        c2.printCN();

    }



    static void Main0(string[] args)
    {
        ComplexNumber c1 = new ComplexNumber(2,6);
        ComplexNumber c2 = new ComplexNumber(1,9);
        ComplexNumber c3 = c1+c2;

        System.Console.WriteLine(c3[0]);
        System.Console.WriteLine(c3[true]);
        

        c3.printCN();
    }
}
