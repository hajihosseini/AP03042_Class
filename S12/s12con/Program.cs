
namespace s12con;

class Program
{

    static void MySort(Teacher[] teachers)
    {
        for(int i=0; i<teachers.Length; i++)
            for(int j=i+1; j<teachers.Length; j++)
                if (teachers[i].IsBigger(teachers[j]))
                    swap<Teacher>(teachers, i, j);
    }

    // private static void swap(Student[] students, int i, int j)
    // {
    //     Student tmp = students[i];
    //     students[i] = students[j];
    //     students[j] = tmp;
    // }    
    static void MySort(Student[] students)
    {
        for(int i=0; i<students.Length; i++)
            for(int j=i+1; j<students.Length; j++)
                if (students[i].IsBigger(students[j]))
                    swap<Student>(students, i, j);
    }

    static void MySort2<_T>(_T[] students, IHasBiggerBetween<_T> cmp)
    {
        for(int i=0; i<students.Length; i++)
            for(int j=i+1; j<students.Length; j++)
                if (cmp.IsBigger(students[i], students[j]))
                    swap<_T>(students, i, j);
    }


   static void MySort<_T>(_T[] items) where _T: IHasBigger<_T>
    {
        for(int i=0; i<items.Length; i++)
            for(int j=i+1; j<items.Length; j++)
                if (items[i].IsBigger(items[j]))
                    swap<_T>(items, i, j);
    }    

    // private static void swaps(Student[] students, int i, int j)
    // {
    //     Student tmp = students[i];
    //     students[i] = students[j];
    //     students[j] = tmp;
    // }

    private static void swap<_T>(_T[] students, int i, int j)
    {
        _T tmp = students[i];
        students[i] = students[j];
        students[j] = tmp;
    }    

    static void Main(string[] args)
    {
        Student[] students = new Student[]{};
        MySort(students);
        Teacher[] teachers = new Teacher[]{};
        MySort(teachers);

        MySort2(students, new StudentIdComparer());
        MySort2(students, new StudentFirstNameComparer());

        MySort2(students, MyComparers.IdComparer);
        MySort2(teachers, MyComparers.IdComparer);

        String s1 = "alk";
        String s2 = "zari";
        string[] names = new string[]{};
        Array.Sort(names, StringComparer.CurrentCulture);
    }
}
