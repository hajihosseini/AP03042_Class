public class Student
{
    public static int StudentCount = 0;
    public string name;
    public int id;

    public Student(string fname, int nid)
    {
        this.name = fname;
        this.id = nid;
        StudentCount ++;
    }

}