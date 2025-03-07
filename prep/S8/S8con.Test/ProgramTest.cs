using System.Diagnostics;

namespace S8con.Test;

[TestClass]
public class ProgramTest
{
    [TestMethod]
    public void TestMethod1()
    {
        int x = S8con.MyProgram.add(1, 1);
        Assert.AreEqual(x, 2);
    }

    [TestMethod]
    public void ReverseTest()
    {
        Assert.Inconclusive();
        string sin = "123456789";
        string eout = "987654321";
        string sact;
        MyProgram.Reverse(sin, out sact);
        Assert.AreEqual(eout, sact);
    }    
}