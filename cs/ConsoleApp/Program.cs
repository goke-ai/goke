// See https://aka.ms/new-console-template for more information
using Goke.Core;
using Markdig.Helpers;
using System.Text;

Console.WriteLine("Hello, World!");


Console.WriteLine($"Generate: {Text.Generate()}");

var query = Chemistry.ELEMENTS.OrderBy(o => o.Value.AtomicNumber).Take(20);

foreach (var q in query)
{
    Console.Write(q.Key);
    Console.Write(", ");
}
Console.WriteLine();

Console.WriteLine($"H2O: {Chemistry.MolarMass(new Dictionary<string, double>() { { "H", 2 }, { "O", 1 } })}");
Console.WriteLine($"H2O: {Chemistry.MolarMass("H2O")}");
Console.WriteLine($"C6H12O6: {Chemistry.MolarMass(new Dictionary<string, double>() { { "C", 6 }, { "H", 12 }, { "O", 6 } })}");
Console.WriteLine($"C6H12O6: {Chemistry.MolarMass("C6H12O6")}");
Console.WriteLine($"CH2Cl2: {Chemistry.MolarMass(new Dictionary<string, double>() { { "C", 1 }, { "H", 2 }, { "Cl", 2 } })}");
Console.WriteLine($"CH3Cl: {Chemistry.MolarMass(new Dictionary<string, double>() { { "C", 1 }, { "H", 3 }, { "Cl", 1 } })}");
Console.WriteLine($"CH3Cl: {Chemistry.MolarMass("CH3Cl")}");
Console.WriteLine($"CH2Cl2: {Chemistry.MolarMass(new Dictionary<string, double>() { { "C", 1 }, { "H", 2 }, { "Cl", 2 } })}");
Console.WriteLine($"CH2Cl2: {Chemistry.MolarMass("CH2Cl2")}");
Console.WriteLine($"CH1.5Cl2.5: {Chemistry.MolarMass(new Dictionary<string, double>() { { "C", 1 }, { "H", 1.5 }, { "Cl", 2.5 } })}");
Console.WriteLine($"CH1.5Cl2.5: {Chemistry.MolarMass("CH1.5Cl2.5")}");


foreach (var item in Chemistry.ToDictionary("CH1.5Cl2.5"))
{
    Console.WriteLine(item);
}


Console.WriteLine();