public class Student: IComparable
{
    private string fname;
    public string FirstName
    {
        get 
        {
            return fname;
        }
        set
        {
            fname = value;
        }
    }
    private string lname;
    public string LastName
    {
        get => lname;
        set => lname = value;
    }

    private int id;
    public int Id {
        get => id;
        set 
        {
            if (Id == value)
                throw new InvalidDataException("id and stdid can't be the same");
            
            if (value <= 0)
                throw new InvalidOperationException("id can't be less than zero");

            this.id = value;                
        }
    }

    private int stdId;
    public int StdId {
        get => stdId;
        set
        {
            if (Id == value)
                throw new InvalidDataException("id and stdid can't be the same");
            stdId = value;
        }
    }
    public static int LastStudentId {private set; get; } = 403521000;
    public double GPA {get; private set;}

    public string FullName => $"{FirstName} {LastName}";

    public Student(string fname, string lname, int Id, double GPA)
        : this(fname, lname, Id, LastStudentId++, GPA)
    {
    }

    public Student(string fname, string lname, int Id, int StdId, double GPA)
    {
        this.fname = fname;
        this.lname = lname;
        if (Id == StdId)
        {
            throw new InvalidDataException("id and stdid can't be the same");
        }
        this.StdId = StdId;
        this.Id = Id;        
        this.GPA = GPA;

    }

    public override string ToString() => 
        $"{FullName}\t{Id}\t{StdId}\t{GPA}";

    public int CompareTo(object obj)
    {
        Student other = obj as Student;
        if (other == null)
            return 1;
        return this.LastName.CompareTo(other.LastName);
    }
}