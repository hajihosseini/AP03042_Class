using System.Formats.Asn1;
using System.Runtime.Intrinsics.Arm;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace S8con.Test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        int x = 1;
        Assert.AreEqual(x+1, 2);
    }

    public void TestMethod2()
    {
        int result = Program.add(3,5);
        Assert.AreEqual(result, 8);
    }

}