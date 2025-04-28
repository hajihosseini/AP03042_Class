namespace S19;
// class HWX
// {
//     static void Test()
//     {
//         Program.Log
//     }
// }
class Program
{
    public delegate int BinaryOp(int a, int b);
    public delegate void LogFn(string msg);

    private static LogFn Log ; 


    static int[] Apply(int[] nums1, int[] nums2, BinaryOp op)
    {
        int[] result = new int[nums1.Length];
        for(int i=0; i<nums1.Length; i++)
            result[i] = op(nums1[i], nums2[i]);
        return result;
    }

    static void LogToFile(string msg)
    {
        File.AppendAllText("log.txt", msg);
    }

    static int add(int a, int b) => a+b;
    static int mul(int a, int b) => a*b;

    static void Do<T>(T t)
    {

    }

    static void Main(string[] args)
    {
        Log += Console.WriteLine;
        Log += LogToFile;

        int[] nums1 = new int[4] {1, 2, 3, 5};
        int[] nums2 = new int[4] {3, 7, 4, 2};
        int[] result = Apply(nums1, nums2, add);
        foreach(var re in result)
            Log?.Invoke(re.ToString());

        Log -= Console.WriteLine;
        Log += msg => Console.WriteLine($"SMS:+989103843818 {msg}");
        Log -= msg => Console.WriteLine($"SMS:+989103843818 {msg}");
        var v = new {Name="Ali", Id=134};
        Do(v);


            // if (null != Log)
            //     Log(re.ToString())

        string s = null;
        string name = s ?? "hassan";
        string name2 = s != null ? s : string.Empty;
        int r = 15;
        int[] result2 = Apply(nums1, nums2, (a, b) => (a*2 - b+ 5) % r);
        foreach(var re in result)
            Log?.Invoke(re.ToString());

        BinaryOp op1 = (int a, int b) => {
            return (a*2 - b+ 5) % r;
        };
        var op2 = (int a, int b) => (a*2 - b+ 5) % r;
    
        Log?.Invoke("Hello, World!");
    }
}
