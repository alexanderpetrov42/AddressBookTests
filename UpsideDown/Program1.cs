string a = Console.ReadLine();
string b = "";

for (int i = a.Length - 1; i < 0; i-- )
{
    string.Join(a[i], b);
}
Console.WriteLine(b);
