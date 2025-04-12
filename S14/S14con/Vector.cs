using System.Numerics;


public class NDVector
{
    public NDVector(int dim, double [] values)
    {

    }

    private double[] Values;

    double this[int n]
    {
        get
        {
            return Values[n];
        }
        set
        {
            Values[n] = value;
        }
    }
}

public interface IVector<T> where T: INumber<T>
{
    T Magnitude {get;}
    Vector<T> Add(IVector<T> o);
    T X {get; set;}
    T Y {get; set;}
}

// public class AngleVector<T>:
//     IEquatable<Vector<T>>, 
//     IComparable<Vector<T>>,
//     IVector<T>
//         where T: INumber<T>
//         {}


public class Vector<T>: 
    IEquatable<Vector<T>>, 
    IComparable<Vector<T>>,
    IVector<T>
        where T: INumber<T>
{
    public Vector(T x, T y)
    {
        this.X = x;
        this.Y = y;
    }
    public T X {get; set;}
    public T Y {get; set;}

    int IComparable<Vector<T>>.CompareTo(Vector<T> v) =>    
     this.Magnitude.CompareTo(v.Magnitude);

    public T Magnitude => X*X + Y*Y;
    public Vector<T> Add(IVector<T> v) => new Vector<T>(X + v.X, Y + v.Y);
    bool IEquatable<Vector<T>>.Equals(Vector<T> v)
    {
        if (null == v)
            return false;        
        return (v.X == this.X) && (v.Y == this.Y);
    }
    public override bool Equals(object obj)
    {        
        Vector<T> v = obj as Vector<T>;
        if (v != null)
            return Equals(v);
        return false;
    }

    public override int GetHashCode()
    {
        return this.X.GetHashCode() ^ this.Y.GetHashCode();
    }

}



        // string s = obj as string;
        // if (s != null)
        // {
        //     v = Vector<T>.Parse(s);
        //     return Equals(v);            
        // }

    // public static Vector<T> Parse(string s)
    // {
    //     string[] tokens = s.Split(',');
    //     return new Vector<T>(T.Parse(tokens[0]), T.Parse(tokens[1]));
    // }