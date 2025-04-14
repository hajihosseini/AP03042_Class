class Student : IComparable<Student>
{

    public static bool operator<(Student a, Student b) => a.CompareTo(b) < 0;
    public static bool operator>(Student a, Student b) => ! (a < b);

    public static bool operator==(Student a, Student b) => a.CompareTo(b) == 0;
    public static bool operator!=(Student a, Student b) => ! (a.CompareTo(b) == 0);

    public static Student[] operator+(Student a, Student b)
    {
        return new Student[]{a, b};
    }
    
    public static Student operator+(Student a, double d)
    {
        a.Akhlagh += d;
        return a;
    }
    
    public double Akhlagh {get; set;}
    public int CompareTo(Student other)
    {
        if (other != null)
            return this.Akhlagh.CompareTo(other.Akhlagh);
        return -1;
    }
}