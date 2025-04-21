using System.Runtime.CompilerServices;

interface IShape
{
    // string Name;    
}

class Vector{ 
    public Vector(int x, int y) {this.X = x; this.Y = y;}
    public int X {get; set;} 
    public int Y {get; set;}
    public static Vector operator+(Vector v1, Vector v2)
    {
        v1.X += v2.X;
        v1.Y += v2.Y;
        return v1;
    }
    public override string ToString() => $"[{X},{Y}]";
    public Vector Clone()
    {
        return new Vector(this.X, this.Y);
    }

}

abstract class Shape
{
    private string Name;
    protected Vector[] points;
    public void Move(Vector d)
    {
        for(int i=0; i<points.Length; i++)
            points[i] += d;
    }

    public void Draw()
    {
        for(int i=1; i<points.Length; i++)
            System.Console.WriteLine($"line({points[i-1]} => {points[i]})");
    }

    public abstract double GetArea();
}

class Square : Shape
{
    public int Len { get; set;}
    public Square(Vector upperleft, int len)
    {
        this.points = new Vector[4];
        this.points[0] = upperleft.Clone();
        this.points[1] = new Vector(upperleft.X + len, upperleft.Y);
        this.points[2] = new Vector(upperleft.X + len, upperleft.Y + len);
        this.points[3] = new Vector(upperleft.X       , upperleft.Y + len);
        this.Len = len;
    }
    public override double GetArea() => this.Len * this.Len;
}
