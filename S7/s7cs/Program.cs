using System.Globalization;

namespace s7cs;

class Program
{
    // static void update(int[] nums)
    // {
    //     for(int i=0; i<nums.Length; i++)
    //         nums[i]++;
    // }
    static void update(ref int nums)
    {
        nums++;
    }
    static void Main(string[] args)
    {
        // int [] num = new int[10] {1,2,3,4,4,5,5,1,1,2};

        // update(num);

        int a = 10;
        update(ref a);
        Console.WriteLine(a);

        // for(int i=0; i<num.Length; i++)
        //     Console.WriteLine(num[i]);


        // for(int i=0; i<100000; i++)
        // {
        //     int[] nums = new int[1000*1000];
        //     Thread.Sleep(1);
        // }

        Console.WriteLine("Hello, World!");
    }
}
