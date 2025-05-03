namespace S20;

class Program
{
    static void Main(string[] args)
    {
        ComplexNumber c1 = new ComplexNumber(2,6);
        ComplexNumber c2 = new ComplexNumber(1,9);
        ComplexNumber c3 = c1+c2;

        System.Console.WriteLine(c3[0]);
        // System.Console.WriteLine(c3[true]);
        

        c3.printCN();
    }
}
