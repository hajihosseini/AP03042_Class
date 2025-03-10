

public class Student
{
    public string name;
    public int stdid;
    public int natid;
    public int credits;
    public bool active;

    public Student(string name, int stdid, int natid, int credits, bool active)
    {
        this.name = name;
        this.stdid = stdid;
        this.natid = natid;
        this.credits = credits;
        this.active = active;
    }

    public static Student Parse(string str)
    {
        string[] tokens = str.Split(',');
        var name = tokens[0];
        var stdid = int.Parse(tokens[1]);
        var natid = int.Parse(tokens[2]);
        var credits = int.Parse(tokens[3]);
        var active = bool.Parse(tokens[4]);
        return new Student(name, stdid, natid, credits, active);

    }

    public override string ToString() =>
        $"{name},{stdid},{natid},{credits},{active}";

    public override bool Equals(object obj)
    {
        if (obj is Student) {
            Student other = obj as Student;
            return 
                name.Equals(other.name)  &&
                stdid.Equals(other.stdid)  &&
                natid.Equals(other.natid)  &&
                active.Equals(other.active)  &&
                credits.Equals(other.credits) ;
        }
        return false;
    }

    // public void printme() => Console.WriteLine(this.ToString());
}