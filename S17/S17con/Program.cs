namespace S17con;

class Program
{
    static void Main(string[] args)
    {
        Vector[] points = new Vector[3] {
                new Vector(1,1), 
                new Vector(1,3),
                new Vector(5,2)};
        List<Square> squares = new List<Square>();
        foreach(var p in points)
            foreach(int l in new int[]{3, 5})
                squares.Add(
                    new Square(p, l)
                );
        foreach(var p in points)
        {
            p.X = 0;
            p.Y = 0;
        }
        foreach(Shape s in squares) {
            s.Draw();
            System.Console.WriteLine("-----------");
        }

        Vector v1 = new Vector(5, 2);
        Square sq1 = new Square(v1, 5);
        v1.X = 2;
        v1.Y = 2;
        Square sq2 = new Square(v1, 6);
    }
}
