namespace S20;



class Program
{
    static bool IsOdd(int a) 
    {
        return a % 2 == 1;
    }
    
    static string L2S(IEnumerable<int> nums)
    {
        return string.Join(" ", nums);
    }
    static bool IsPrime(int n)
    {
        for(int i = 2; i<n; i++)
            if (n % i == 0)
                return false;
        return true;
    }
    static void Main(string[] args)
    {
        var data = Enumerable.Range(1,100)
                  .Where(x => IsPrime(x))
                  .Select( x => (
                    num: x,
                    key: x.ToString().Select(c => int.Parse(c.ToString())).Sum()
                  ));
        data.Join(data, t => t.key, t => t.key, (t1, t2) => (n1: t1.num, n2: t2.num, k:t1.key))
                  .Where( t => t.n1 != t.n2)
                  .DistinctBy(t => t.n1 < t.n2 ? (t.n1,t.n2).ToString() : (t.n2,t.n1).ToString())
                  .ToList()
                  .ForEach(x => Console.WriteLine(x));
        return;
        Enumerable.Range(1,100)
                  .Where(x => IsPrime(x))
                  .Select( x => (
                    x,
                    x.ToString().Select(c => int.Parse(c.ToString())).Sum()
                  ))
                  .ToList()
                  .ForEach(x => Console.WriteLine(x));

        return;

        Enumerable.Range(0,100)
                  .GroupBy(x => x/10)
                  .Select(ig => (
                        ig.Key, 
                        ig.Where(x => x % 2 == 1).Average()
                    ))
                  .ToList()
                  .ForEach(x => Console.WriteLine(x));
        return;


        Enumerable.Range(0,100)
                  .GroupBy(x => x/10)
                  .Select(ig => string.Join(ig.Key < 1 ? "  ": " ", ig))
                  .ToList()
                  .ForEach(Console.WriteLine);
        return;


        Enumerable.Range(1,100)
                  .Where(IsOdd)
                  .ToList()
                  .ForEach(Console.WriteLine);
        
    }
}
