
int w = 0;
char c = 'A';
double d = 0.5;
long l = 1;
ulong ll = 2;

var chars = new char[5];
// char * chars = malloc(5*sizeof(char));

int i = 10;
var n = 15;
while (i > 0)
{
    if (i % 2 == 1)
        Console.WriteLine("great! " + i);
    else
        Console.WriteLine("Skip");
    i--;
}


for(int j=0; j<3; i++)
{
    Console.Write("name? ");
    var name = Console.ReadLine();
    Console.WriteLine("Hello, " + name + " !" );
}
