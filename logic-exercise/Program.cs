int n = 105;
for (int i = 1; i <= n; i++)
{
    bool printed = false;

    if (i % 3 == 0)
    {
        Console.Write("foo");
        printed = true;
    }
    if (i % 4 == 0)
    {
        Console.Write("baz");
        printed = true;
    }
    if (i % 5 == 0)
    {
        Console.Write("bar");
        printed = true;
    }
    if (i % 7 == 0)
    {
        Console.Write("jazz");
        printed = true;
    }
    if (i % 9 == 0)
    {
        Console.Write("huzz");
        printed = true;
    }
    if (!printed)
    {
        Console.Write(i);
    }

    if (i < n)
    {
        Console.Write(", ");
    }
}