
public class Student: IDisposable
{
    bool isMale;
    private string FirstName;
    public void set_Name(string newName)
    {
        if (newName.Length <= 20)
            this.FirstName= newName;
    }
    public string get_Name()
    {
        string result;
        if (this.isMale)
            result = "Mr. ";
        else
            result = "Ms. ";
        return result + this.FirstName;
    }

    private string _LastName;
    public string LastName
    {
        get
        {
            return _LastName;
        }
        set
        {
            this._LastName = value;
        }
    }

    public string FullName
    {
        get
        {
            return this.FirstName +  " " + this.LastName;
        }
    }


    public Student(string name)
    {
        this.FirstName = name;
        Register(name);
    }

    ~Student()
    {
        Console.WriteLine($"is deallocated");
    }

    private void Register(string name)
    {
        System.Console.WriteLine($"{name} is registered");
    }

    public void Dispose()
    {
        System.Console.WriteLine($"{this.FirstName} is unregistered");
    }


}