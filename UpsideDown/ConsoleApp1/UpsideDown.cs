string a = Console.ReadLine();
string b = "";

for (int i = 0; i < a.Length; i++)
{
    b = string.Concat(a[i],b);
}
Console.WriteLine(b);

