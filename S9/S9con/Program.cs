using System.Reflection;
using System.Runtime.InteropServices;

namespace S9con;

class Program
{
    static void Main(string[] args)
    {
        int x = int.Parse("534");
        int y = 5;
        if (y.CompareTo(x) > 0)
            System.Console.WriteLine("-");
        Console.WriteLine(y.ToString());

        int i = 5;
        object o = i;
        int n = (int) o;

        Student s = new Student(
            name:"Zhale Hosseini", 
            stdid: 403521, 
            natid: 837743647, 
            credits: 17,
            active: true);

        string str = "Zhale Hosseini,403521,837743647,17,True";
        Student sc = Student.Parse(str);
        System.Console.WriteLine(sc.Equals(1234));

        //File.WriteAllLines("test.csv", new string[]{s.ToString(), sc.ToString()});
        string[] lines = File.ReadAllLines("test.csv");
        foreach(var line in lines)
        {
            var ss = Student.Parse(line);
            System.Console.WriteLine(ss.credits);
        }

    }
}
