public class ComplexNumber
{
    public double Re;
    public double Im;

    public ComplexNumber(double r, double i)
    {
        this.Re = r;
        this.Im = i;
    }

    public void printCN()
    {
        System.Console.WriteLine($"{this.Re}+{this.Im}i");
    }

    public static ComplexNumber operator+(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Re+b.Re, a.Im+b.Im);
    }

    public double this[int index]
    {
        get
        {
            if(index==0)
                return this.Re;
            else if(index==1)
                return this.Im;
            else
                throw new IndexOutOfRangeException("index out of range!");
        }

        set
        {
            if (index==0)
                this.Re = value;
            else if(index==1)
                this.Im = value;
            else
                throw new IndexOutOfRangeException("index out of range!");
            
        }
    }
}