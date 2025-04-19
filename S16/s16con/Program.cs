namespace s16con;

class Program
{
    static void print_citizen(Citizen[] cs)
    {
        foreach(var c in cs)
            Console.WriteLine(c.ToString());
    }
    static void Main(string[] args)
    {
        Citizen k = new Citizen("Kane", "Dave", 1, true);
        Student s = new Student(
            "Zari", "Hedayati", 2, "Math", 403521, false);
        GradStudent gs = new GradStudent(
            "Pari", "Hedayati", 2, "Math", 403521, false,
            "Calculus", "Integral");
        Citizen[] citizens = new Citizen[]{k, s, gs};
        // print_citizen(citizens);
        // System.Console.WriteLine(k);
        // System.Console.WriteLine(s);
        // System.Console.WriteLine(gs);
        s.Register("Numericals");
        gs.Register("Numericals 2");

    }
}
