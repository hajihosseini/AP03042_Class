namespace S12;

class Program
{
    static void Main(string[] args)
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
