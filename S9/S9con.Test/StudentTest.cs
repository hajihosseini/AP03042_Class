namespace S9con.Test;

[TestClass]
public class StudentTest
{
    [TestMethod]
    public void ParseTest()
    {
        Student s = new Student(
            name:"Zhale Hosseini", 
            stdid: 403521, 
            natid: 837743647, 
            credits: 17,
            active: true);

        string str = "Zhale Hosseini,403521,837743647,17,True";
        Student sc = Student.Parse(str);
        Assert.AreEqual(s, sc);
    }
}