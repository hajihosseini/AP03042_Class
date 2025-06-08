using System.ComponentModel;
using System.Text.RegularExpressions;

namespace S25;

class Program
{
    static void Main3343(string[] args)
    {
        using var client = new HttpClient();
        string result =
            client.GetStringAsync("https://www.tabnak.ir/").Result;
        File.WriteAllText("index.html", result);

    }
}