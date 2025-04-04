using System.Security.Cryptography.X509Certificates;

public interface IHasBigger<_T>
{
    bool IsBigger(_T other);
}

public interface IHasBiggerBetween<_T>
{
    bool IsBigger(_T s1, _T s2);
}


public interface IHasId
{
    int Id {get;}
}

public class StudentIdComparer: IHasBiggerBetween<Student>
{
    public bool IsBigger(Student s1, Student s2)
    {
        return s1.Id > s2.Id;
    }
}

public class TeacherIdComparer: IHasBiggerBetween<Teacher>
{
    public bool IsBigger(Teacher s1, Teacher s2)
    {
        return s1.Id > s2.Id;
    }
}

public class IdComparer : IHasBiggerBetween<IHasId>
{
    public bool IsBigger(IHasId s1, IHasId s2)
    {
        return s1.Id > s2.Id;
    }
}

public static class MyComparers
{
    public static IdComparer IdComparer = new IdComparer();
    // public static StudentIdComparer StudentIdComparer = new StudentIdComparer(); 
    public static StudentFirstNameComparer StudentFirstNameComparer = new StudentFirstNameComparer(); 
    // public static TeacherIdComparer TeacherIdComparer = new TeacherIdComparer();
}

public class StudentFirstNameComparer: IHasBiggerBetween<Student>
{
    public bool IsBigger(Student s1, Student s2)
    {
        return s1.FirstName.CompareTo(s2.FirstName) > 0;
    }
}

public class Student: IHasId
{
    public int Id {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}

    public double GPA {get; set;}
    public bool IsBigger(Student other)
    {
        return this.FirstName.CompareTo(other.FirstName) < 0;            
    }
}

public class Teacher: IHasId
{
    public int Id {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}

    public double Rating {get; set;}
    public bool IsBigger(Teacher other)
    {
        return this.Id > other.Id;            
    }
}