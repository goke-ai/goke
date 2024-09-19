// See https://aka.ms/new-console-template for more information
using Goke.Core;
using Markdig.Helpers;
using System.Text;
using System.Text.Json;

Console.WriteLine("Hello, World!");

Console.WriteLine($"Generate: {Text.Generate()}");

var query = Chemistry.Elements1To20;

foreach (var q in query)
{
    Console.Write(q.Symbol);
    Console.Write(", ");
}
Console.WriteLine();

Console.WriteLine($"1-20: {string.Join(", ", Chemistry.Elements1To20.Select(s => s.Symbol))}");
Console.WriteLine($"Halogens: {string.Join(", ", Chemistry.Halogens.Select(s => s.Symbol))}");
Console.WriteLine($"Group1: {string.Join(", ", Chemistry.Group1.Select(s => s.Symbol))}");
Console.WriteLine($"Group4: {string.Join(", ", Chemistry.Group4.Select(s => s.Symbol))}");
Console.WriteLine($"Transition Metals: {string.Join(", ", Chemistry.TransitionMetals.Select(s => s.Symbol))}");

Console.WriteLine();
Console.WriteLine($"Oxidation State: H2: {Chemistry.OxidationState("H2")}");
Console.WriteLine($"Oxidation State: O: {Chemistry.OxidationState("O")}");
Console.WriteLine($"Oxidation State: Cl: {Chemistry.OxidationState("Cl")}");
Console.WriteLine($"Oxidation State: CH4: {Chemistry.OxidationState("CH4")}");
Console.WriteLine($"Oxidation State: SO3: {Chemistry.OxidationState("SO3")}");
Console.WriteLine($"Oxidation State: SO4: {Chemistry.OxidationState("SO4")}");

Console.WriteLine();
var H = Chemistry.GetElementBySymbol("H");
Console.WriteLine($"H: {H}");
// H.BoilingPoint = 300;
Console.WriteLine($"Hydrogen: {Chemistry.GetElementByName("Hydrogen")}");
Console.WriteLine($"hydrogen: {Chemistry.GetElementByName("hydrogen")}");

Console.WriteLine();
Console.WriteLine($"H2O: {Chemistry.MolarMass(new Dictionary<string, double>() { { "H", 2 }, { "O", 1 } })}");
Console.WriteLine($"H2O: {Chemistry.MolarMass("H2O")}");
Console.WriteLine($"C6H12O6: {Chemistry.MolarMass(new Dictionary<string, double>() { { "C", 6 }, { "H", 12 }, { "O", 6 } })}");
Console.WriteLine($"C6H12O6: {Chemistry.MolarMass("C6H12O6")}");
Console.WriteLine($"CH3Cl: {Chemistry.MolarMass(new Dictionary<string, double>() { { "C", 1 }, { "H", 3 }, { "Cl", 1 } })}");
Console.WriteLine($"CH3Cl: {Chemistry.MolarMass("CH3Cl")}");
Console.WriteLine($"CH2Cl2: {Chemistry.MolarMass(new Dictionary<string, double>() { { "C", 1 }, { "H", 2 }, { "Cl", 2 } })}");
Console.WriteLine($"CH2Cl2: {Chemistry.MolarMass("CH2Cl2")}");
Console.WriteLine($"CH1.5Cl2.5: {Chemistry.MolarMass(new Dictionary<string, double>() { { "C", 1 }, { "H", 1.5 }, { "Cl", 2.5 } })}");
Console.WriteLine($"CH1.5Cl2.5: {Chemistry.MolarMass("CH1.5Cl2.5")}");
Console.WriteLine($"NaOH: {Chemistry.MolarMass(new Dictionary<string, double>() { { "Na", 1 }, { "O", 1 }, { "H", 1 } })}");
Console.WriteLine($"NaOH: {Chemistry.MolarMass("NaOH")}");
Console.WriteLine($"SeO: {Chemistry.MolarMass(new Dictionary<string, double>() { { "Se", 1 }, { "O", 1 }})}");
Console.WriteLine($"SeO: {Chemistry.MolarMass("SeO")}");


foreach (var item in Chemistry.ParseCompound("CH1.5Cl2.5"))
{
    Console.WriteLine(item);
}

Console.WriteLine();

var options = new JsonSerializerOptions { WriteIndented = true }; 
string jsonString = JsonSerializer.Serialize(Chemistry.AllElements, options);
Console.WriteLine(jsonString);

var path = "periodic.json";
File.WriteAllText(path, jsonString);

Console.WriteLine();

string fileName = "C:\\Users\\gokel\\OneDrive\\zs\\goke\\cs\\Goke.Core\\periodic_table.json";


using FileStream openStream = File.OpenRead(fileName);
List<Chemistry.Element>? elements = await JsonSerializer.DeserializeAsync<List<Chemistry.Element>>(openStream);
Console.WriteLine($"Element 20: {elements?[19]}");


Console.WriteLine($"Text: {Text.GeneratePin()}");
Console.WriteLine($"Text: {Text.MakePinReadable(Text.GeneratePin())}");
Console.WriteLine($"Text: {Text.MakePinReadable(Text.GeneratePin(),3)}");



