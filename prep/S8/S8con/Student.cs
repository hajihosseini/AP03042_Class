using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public class Student
{
    static private int TotalStudentCount = 0;
    private string Name;
    private int Id;

    public int Age;

    private double[] grades = new double[10];
    int last_grade=0;

    public void add_grade(double g)
    {
        grades[last_grade] = g;
        last_grade++;
    }

    public Student(string name, int id, int age=18)
    {
        TotalStudentCount++;
        Name = name;
        Id = id;
        Age = age;
    }

    public static int GetTotalStudentCount()
    {
        return TotalStudentCount;
    }


}