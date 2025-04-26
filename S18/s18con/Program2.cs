
class AddClosure
{
    private int Remainder;
    public AddClosure(int remainder)
    {
        this.Remainder = remainder;
    }
    public int add(int a, int b)
    {
        return (a + b) % Remainder;
    }
}

class Program
{
    delegate T fn_int_2int<T>(T a, T b);

    static void apply<T>(T[] nums1, T[] nums2, T[] result, Func<T, T, T> fn)
    {
        for(int i=0; i<nums1.Length; i++)
        {
            result[i] = fn(nums1[i], nums2[i]);
        }
    }
    // static void apply<T>(T[] nums1, T[] nums2, T[] result, fn_int_2int<T> fn)
    // {
    //     for(int i=0; i<nums1.Length; i++)
    //     {
    //         result[i] = fn(nums1[i], nums2[i]);
    //     }
    // }

    static int add(int a, int b) => a + b;
    static int mul(int a, int b) => a * b;

    static void dodo<T>(T[] values, Action<T> a)
    {
        foreach(var v in values)
            a(v);
    }

    static void w2f<T>(T v)
    {
        string filename = "a.txt";
        if (v != null)
            File.AppendAllText(filename, v.ToString() + "\n");
    }

    static void Main(string[] args)
    {
        int[] n1 = new int[3]  {10, 20, 30};
        int[] n2 = new int[3]  {-1, -2, -3};
        int[] r = new int[3];

        for(int i=5; i<10;) {
            apply(n1, n2, r, (a, b) => {
                i++;    
                return (a + b) % i;
            }
            );
            dodo(r, System.Console.Write);
            System.Console.WriteLine();
        }

        return;








        apply(n1, n2, r,  new AddClosure(5).add);
        dodo(r, System.Console.WriteLine);
        apply(n1, n2, r,  new AddClosure(7).add);
        dodo(r, System.Console.WriteLine);
        return;





















        var f = (int a, int b) => a * b + a - 2;
        
        apply(n1, n2, r, (a, b) => a * b + a - 2);
        // foreach(var n in r)
        //     System.Console.WriteLine(n);
        dodo(r, System.Console.WriteLine);
        dodo(r, w2f);
    }
}