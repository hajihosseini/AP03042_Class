namespace S11con.Test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void TestMethod1()
    {
        Student s = new Student("ali", "sdf", 23, 23, 19.5);
    }
}