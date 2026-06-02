using System;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main()
    {
        var asm = Assembly.Load("ShapeCrawler");
        foreach (var type in asm.GetTypes().OrderBy(t => t.FullName))
        {
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
            {
                if (method.Name.Contains("AddPicture"))
                {
                    Console.WriteLine($"Type: {type.FullName}");
                    Console.WriteLine($"  {method}");
                }
            }
        }
    }
}
