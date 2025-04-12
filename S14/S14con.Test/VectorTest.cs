namespace S14con.Test;

[TestClass]
public class VectorTest
{
    [TestMethod]
    public void AddTest()
    {
        Vector<int> v1 = new Vector<int>(3, 4);
        Vector<int> v2 = new Vector<int>(1, 2);
        
        Vector<int> vsum = v1.Add(v2);
        Vector<int> vExpectedSum = new Vector<int>(4, 6);

        Assert.AreEqual(vsum, vExpectedSum);
        // Assert.AreEqual(vsum, "4,6");


        // Vector v3 = new Vector(1,1);
        // Vector vsum2 = v3.Add(v1);
        // Assert.AreEqual(vsum2.X, 4);
        // Assert.AreEqual(vsum2.Y, 5);
        
        
    }
}