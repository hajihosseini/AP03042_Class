using System.Globalization;

namespace S12;

class Program
{

    public static void MySort<T>(T[] plist) where T: IhasHigher<T>
    {
        for(int i=0; i<plist.Length;i++)
        {
            for(int j=i+1; j<plist.Length;j++)
            {
                if(plist[i].IsHigher(plist[j]))
                {
                    swap(plist, i, j);
                }
            }
        }
    }

    private static void swap<T>(T[] plist, int i, int j)
    {
        T temp = plist[i];
        plist[i] = plist[j];
        plist[j] = temp;
    }

    static void Main(string[] args)
    {
        Student[] students = new Student[3]{
            new Student("maryam","akbari", 1233),
            new Student("zoha", "saberi", 1531),
            new Student("narges", "hosseini", 1009)
        };

        for(int i=0; i<students.Length;i++)
            students[i].PrintStudent();
        
        System.Console.WriteLine("------------------------");

        MySort(students);

        for(int i=0; i<students.Length;i++)
            students[i].PrintStudent();

        Teacher[] teachers = new Teacher[3]{
            new Teacher("maryam","akbari", 1233),
            new Teacher("zoha", "saberi", 1531),
            new Teacher("narges", "hosseini", 1009)
        };

        for(int i=0; i<teachers.Length;i++)
            teachers[i].PrintStudent();
        
        System.Console.WriteLine("------------------------");

        MySort(teachers);

        for(int i=0; i<teachers.Length;i++)
            teachers[i].PrintStudent();



    }

    static void Main2(string[] args)
    {
        People p1 = new People("zahra", "mahdavi", 1234);
        People p2 = new People("ali", "hosseini", 1114);
        People p3 = new People("mahsa", "jamshidi", 3234);
       

        List<People> people = new List<People>();
        people.Add(p1);
        people.Add(p2);
        people.Add(p3); 


        for(int i=0; i<people.Count();i++)
        {
            System.Console.WriteLine(people[i].fname);
        }

        people.Sort();

        for(int i=0; i<people.Count();i++)
        {
            System.Console.WriteLine(people[i].fname);
        }


    }
    static void Main1(string[] args)
    {
        IShape[] shapes = new IShape[]{
            new Rectangle(2,3),
            new Circle(8),
            new Rectangle(7,9),
            new Circle(5)
        };
        
        double Area_sum = 0;
        double P_sum = 0;

        for(int i=0; i<shapes.Length;i++)
        {
            P_sum += shapes[i].Perimeter();
            Area_sum += shapes[i].Area();
        }

        System.Console.WriteLine(Area_sum);
        System.Console.WriteLine(P_sum);

    }
    static void Main0(string[] args)
    {
        using(MyTimer time = new MyTimer("Add 0 to 100"))
        {
            int sum = 0;
            for(int i=0; i<100;i++)
            {
                sum += i;
            }
            System.Console.WriteLine(sum);
        }
    }
}
