using System.Numerics;

public static class Basic
{
    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
}

public class ComplexNumber<T> where T : INumber<T>
{
    public T Real;
    public T Img;

    public ComplexNumber(T r, T i)
    {
        this.Real = r;
        this.Img = i;
    }

    public override string ToString()
    {
        return $"{this.Real}+{this.Img}i";
    }

    public static ComplexNumber<T> operator +(ComplexNumber<T> a, ComplexNumber<T> b)
    {
        return new ComplexNumber<T>(a.Real + b.Real, a.Img + b.Img);
    }
}