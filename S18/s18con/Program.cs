using System.Security.Cryptography;

namespace s18con;

class Student
{
    // public Student()
    // {
    //     this.FirstName = string.Empty;
    // }
    public string? FirstName;
    public Nullable<int> Id;
}

class Program
{

    static void print_student(Student? s)
    {
        if (s != null) {
            System.Console.WriteLine($"{s.FirstName}");
            if (s.Id.HasValue)
                System.Console.WriteLine(s.Id.Value);
        }
    }
    static void Main1(string[] args)
    {
        Student? s = null;
        print_student(s);
        Console.WriteLine("Hello, World!");
    }
}
