using System.Collections.Specialized;

public interface IhasHigher<T>
{
    bool IsHigher(T std);   
}

public class Student:IhasHigher<Student>
{
    public string fname;
    public string lname;
    public int NID;

    public Student(string fn, string ln, int id)
    {
        fname = fn;
        lname = ln;
        NID = id;
    }

    public void PrintStudent()
    {
        System.Console.WriteLine($"{this.fname}, {this.lname}, {this.NID}");
    }

    public bool IsHigher(Student std)
    {
        return this.NID>std.NID;
    }
}


public class Teacher:IhasHigher<Teacher>
{
    public string fname;
    public string lname;
    public int NID;

    public Teacher(string fn, string ln, int id)
    {
        fname = fn;
        lname = ln;
        NID = id;
    }

    public void PrintStudent()
    {
        System.Console.WriteLine($"{this.fname}, {this.lname}, {this.NID}");
    }

    public bool IsHigher(Teacher std)
    {
        return this.NID>std.NID;
    }
}