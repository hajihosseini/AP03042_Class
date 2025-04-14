using System.Numerics;

namespace S15con;

class Program
{
    static void swap<T>(ref T a, ref T b)
    {
        T tmp = a;
        a = b;
        b = tmp;
    }

    static T max<T>(T a, T b)
        where T: IComparable<T>
    {
        if (a.CompareTo(b) < 0)
            return b;
        return a;
    }

    static Student max(Student a, Student b)
    {
        if (b > a)
            return b;
        return a;
    }    

    static T sum<T>(T a, T b) where T: INumber<T>
    {
        return (a+b);
    }

    // TODO: sum of INumbers
    static T sum<T>(T[] nums) where T:INumber<T>
    {
        T sum = T.Zero;
        for(int i = 0; i< nums.Length; i++)
            sum = sum + nums[i];
        return sum;
    }

    static T sum<T>(IEnumerable<T> items)
        where T: INumber<T>
    {
        T sum = T.Zero;
        foreach(T i in items)
            sum = sum + i;
        return sum;
    }


    static void PrintItems<T>(IEnumerable<T> items)
    {
        foreach(T i in items)
            System.Console.WriteLine(i);
    }

    static void PrintItemsMe<T>(IEnumerable<T> items)
    {

        IEnumerator<T> eor = items.GetEnumerator();
        //var eor1 = items.GetEnumerator();
        while (eor.MoveNext())
        {
            System.Console.WriteLine(eor.Current);
        }

    }

    static void Main(string[] args)
    {
        int[] nums = new int[]{3, 4, 5, 6};
        double[] numsd = new double[]{3.1, 4.2, 5.5, 6.3};

        int sum = sum<int>(nums);
        System.Console.WriteLine(sum);

        double dsum = sum<double>(numsd);
        List<int> numsl = new List<int>();
        numsl.Add(1);
        numsl.Add(5);
        numsl.Add(4);
        Stack<int> numstack = new Stack<int>();
        numstack.Push(4);
        numstack.Push(2);
        numstack.Push(5);
        int n = numstack.Pop();
        // int nn = numstack[5];
        Queue<int> qnums = new Queue<int>();
        qnums.Enqueue(1);
        qnums.Enqueue(3);
        qnums.Enqueue(5);
        

        System.Console.WriteLine("queue");
        PrintItemsMe(qnums);
        System.Console.WriteLine("stack");
        PrintItemsMe(numstack);
        System.Console.WriteLine("array");
        PrintItemsMe(nums);
        System.Console.WriteLine("list");
        PrintItemsMe(numsl);
    }
    static void Main2(string[] args)
    {
        int a = 5;
        int b = 6;

        string sa = "ali";
        string sz = "zali";
        Student ssa = null , ssb = null;

        Student c = max<Student>(ssa, ssb);
        System.Console.WriteLine(c);

        // System.Console.WriteLine($"{a} , {b}");
        // swap<int>(ref a, ref b);
        // System.Console.WriteLine($"{a} , {b}");
    }
}
