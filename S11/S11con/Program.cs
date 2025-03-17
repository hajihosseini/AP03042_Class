using System.Data.Common;
using System.Linq.Expressions;

namespace S11con;

class Program
{
    static void Method4()
    {
        System.Console.WriteLine("in method 4 enter");
        throw new FileNotFoundException();
        System.Console.WriteLine("in method 4 exit");
    }    

    static void Method3()
    {
        System.Console.WriteLine("in method 3 enter");
        Method4();
        System.Console.WriteLine("in method 3 exit");
    }
    static void Method2()
    {
        System.Console.WriteLine("in method 2 enter");
        try
        {
            Method3();
        }
        catch(Exception e)
        {
            System.Console.WriteLine($"in catch method 2 \n{e.StackTrace}");
            throw;
        }
        System.Console.WriteLine("in method 2 exit");
    }
    static void Method1()
    {
        System.Console.WriteLine("in method 1 enter");
        Method2();
        System.Console.WriteLine("in method 1 exit");
    }


    static void Main(string[] args)
    {
        System.Console.WriteLine("in main enter");
        try
        {
            Method1();
        }
        catch(Exception e)
        {
            System.Console.WriteLine($"in catch main \n{e.StackTrace}");
        }
        System.Console.WriteLine("in main exit");
    }

    static void Main44(string[] args)
    {
        try
        {
            Student s = new Student(
                fname: "Zari",
                lname: "Mousavi",
                Id: 3423432,
                StdId: 403521123,
                GPA: 18.5
            );
            System.Console.WriteLine(s);        
            Student s2 = new Student(
                fname: "Zari",
                lname: "Mousavi",
                Id: 3423432,
                StdId: 403521121,
                GPA: 18.5
            );
            s2.StdId = 3423432;
            System.Console.WriteLine(s2);          
        }
        catch(InvalidDataException)
        {
            System.Console.WriteLine("some error happened.");
        }
    }

    static void Main2(string[] args)
    {
        Student s = new Student(
            fname: "Zari",
            lname: "Mousavi",
            Id: 3423432,
            StdId: 403521123,
            GPA: 18.5
        );

        List<Student> students = new List<Student>();
        students.Add(s);
        students.Add( new Student(
            fname: "Hassan",
            lname: "Ghofrani",
            Id: 223432,
            StdId: 93521123,
            GPA: 12.5
        ));
        students.Add( new Student(
            fname: "Pari",
            lname: "Bazeghi",
            Id: 3423432,
            StdId: 40352112,
            GPA: 16.5
        ));
        students.Add( new Student(
            fname: "Bari",
            lname: "Bousavi",
            Id: 2423432,
            StdId: 403521113,
            GPA: 18.5
        ));
        students.Add( new Student(
            fname: "Ali",
            lname: "Zokaei",
            Id: 33334,
            StdId: 403521123,
            GPA: 18.5
        ));
        students.Add( new Student(
            fname: "Payam",
            lname: "Sabzi",
            Id: 1423432,
            StdId: 403529,
            GPA: 20.0
        ));

        // foreach(Student ss in students)
        //     System.Console.WriteLine(ss);

        // students.Sort();
        // System.Console.WriteLine("----------------------------");

        // foreach(Student ss in students)
        //     System.Console.WriteLine(ss);
        int sum = 0;
        int loopCount = 0;
        while (true)
        {
            Console.Write("Enter Number: ");
            try
            {  
                checked
                {
                    loopCount++;              
                    int n = int.Parse(Console.ReadLine());
                    sum+=n;
                    System.Console.WriteLine($"thanks for entering number {n}");
                    System.Console.WriteLine($"sum {sum} divided by n is {sum / n}");
                }


                // System.Console.WriteLine(loopCount);
            }
            // catch(Exception e)
            // {
            //     System.Console.WriteLine($"Error. Try Again {e.Message}");
            // }
            catch(OverflowException)
            {
                System.Console.WriteLine("Number is too big.");
                System.Console.WriteLine($"sum divided by n is {sum / 1}");
                // System.Console.WriteLine(loopCount);
                continue;
            }
            catch(DivideByZeroException)
            {
                System.Console.WriteLine("You entered zero, for division 1 will be used instead");
                System.Console.WriteLine($"sum divided by n is {sum / 1}");
                // System.Console.WriteLine(loopCount);
                continue;
            }
            catch(FormatException fe)
            {
                System.Console.WriteLine($"{fe.Message}, try again");
                // System.Console.WriteLine(loopCount);
                continue;
            }
            finally
            {
                System.Console.WriteLine(loopCount);
            }
        }


    }
}

