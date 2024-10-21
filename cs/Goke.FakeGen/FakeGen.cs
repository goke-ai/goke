using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Goke.FakeGen;

public static class FakeGen
{
    static Random rand = new Random();

    static List<string> DEPARTMENTCODE => new() { "CHE", "MEE" };

    static List<string> SURNAMES => new(){
            "Oladokun", "Bridgwater", "Adeyemi", "Agbolade", "Williams",
        "Obi", "Ayeni", "Abu", "Abdulahi", "Adams", "Ajanaku",
        "Oni", "Babalola", "Ayoola", "Fatoki", "Efevbokhan"};

    static List<string> NAMES => new(){
            "Tope", "Tim", "Yemi", "Lola", "Tom",
        "Uche", "Yomi", "Zara", "Musa", "Eve",
        "Bimbo", "Kate", "Karen", "Ruby", "Lolade",
        "John", "Judas", "Solomon", "James", "Matthew",
        "Paul", "Zach", "Kole", "Kola", "Max",
        "Tunde", "Bola", "Ola", "Goke", "Efe"};

    static List<char> GENDERS => new() { 'O', 'M', 'F' };

    static List<string> DOMAINS => new() { "com", "net", "io", "live", "edu" };

    static List<string> COMPANYDOMAINS => new() { "ark", "microsoft", "techiegeek", "google", "github" };

    static List<string> COURSENAMES => new(){
            "Mathematical", "Kinetics", "Thermodynamics", "Operational", "Research",
        "Mechanics", "Fluid", "Computational", "Dyanmics", "Modelling",
        "Simulation", "Control", "Method", "Calculus", "Integral",
        "Basic", "Elementary", "Reaction", "Corrosion", "Particulate",
        "Energy", "Balance", "Chemical", "Phenomina", "Food",
        "Anatomy", "Science", "Engineering", "Technology", "Political"};

    static List<string> PAYTRANSACTIONS1 => new(){
         "bought goods {0}",
         "get expensive {0}",
    };

    static List<string> PAYTRANSACTIONS2 => new(){
         "paid some {0} bill",
         "transfer some money to {0}"
    };

    static List<string> RECEIVETRANSACTIONS1 => new(){
         "sold goods {0}",
         "sold expensive {0}",
    };

    static List<string> RECEIVETRANSACTIONS2 => new(){
         "deposit some additional spending money for {0}",
         "received some money from {0}"
    };

    static List<string> ITEMS1 => new()
        {
        "Book", "Furniture", "Pots", "Cutleries", "Cloth", "Shirt", "Jean", "Medical",
        "FRESH PRODUCE","Apples","Bananas","Strawberries","Avocados","Bell Peppers","Carrots","Broccoli","Garlic","Lemons/Limes","Onion","Parsley","Cilantro","Basil","Potatoes","Spinach","Tomatoes",
            "GRAINS","Breadcrumbs","Pasta","Quinoa","Rice","Sandwich Bread","Tortillas",
            "MEAT/PROTEIN","Chicken","Eggs","Ground Beef","Sliced Turkey","Lunch Meat",
            "DAIRY","Butter","Sliced Cheese","Shredded Cheese","Milk","Sour Cream","Greek Yogurt",
            "BAKING GOODS","Baking powder","Baking Soda","Granulated Sugar","Brown Sugar","Flour","Honey","Vanilla Extract","Dry Yeast","Chocolate Chips","Cocoa Powder","Powdered Sugar",
            "FREEZER","Fruit and Berries","Frozen Veggies","Juice Concentrate","Pizza","Pie Crust","Cookie Dough",
            "CANNED/DRIED GOODS","Chicken stock/broth","Salsa","Diced Tomatoes","Jam/Jelly","Peanut Butter","Pasta Sauce","Beans","Soups","Tuna","Green Chiles","Canned Veggies","Coffee", "Tea",
            "CONDIMENTS/SPICES","Black Pepper","Chili Powder","Cinnamon","Crushed Red Pepper","Cumin","Garlic Powder","Ketchup","Mustard","Mayo","Nutmeg","Paprika","Salt","Soy Sauce","Steak Sauce","Hot Sauce/Buffalo Sauce","Salad Dressings",
            "OILS/VINEGARS","Apple cider vinegar","Balsamic vinegar","Coconut Oil","Olive Oil","Vegetable/Canola Oil","Red Wine Vinegar","White Vinegar","Cooking Wine","White Wine Vinegar",
            "SNACKS","Crackers","Nuts","Quick Oats","Popcorn","Tortilla Chips","Cereal",
    };

    static List<string> ITEMS2 => new()
        {
            "Electric", "Water", "Telephone", "Internet", "Suscriptions", "Fun", "Holiday", "Medical"
        };

    public static string GetPAYTRANSACTIONS()
    {
        int n = Random.Shared.Next();

        string result;
        if (n % 2 == 0)
        {
            var x = Random.Shared.Next(0, PAYTRANSACTIONS1.Count);
            var y = Random.Shared.Next(0, ITEMS1.Count);
            result = String.Format(PAYTRANSACTIONS1[x], ITEMS1[y]);
        }
        else
        {
            var x = Random.Shared.Next(0, PAYTRANSACTIONS2.Count);
            var y = Random.Shared.Next(0, ITEMS2.Count);
            result = String.Format(PAYTRANSACTIONS2[x], ITEMS2[y]);
        }
        return result;
    }

    public static string GetRECEIVETRANSACTIONS()
    {
        int n = Random.Shared.Next();

        string result;
        if (n % 2 == 0)
        {
            var x = Random.Shared.Next(0, RECEIVETRANSACTIONS1.Count);
            var y = Random.Shared.Next(0, ITEMS1.Count);
            result = String.Format(RECEIVETRANSACTIONS1[x], ITEMS1[y]);
        }
        else
        {
            var x = Random.Shared.Next(0, RECEIVETRANSACTIONS2.Count);
            var y = Random.Shared.Next(0, ITEMS2.Count);
            result = String.Format(RECEIVETRANSACTIONS2[x], ITEMS2[y]);
        }
        return result;
    }

    public static double GetMONEY(int max = 100)
    {
        double randomDouble = Random.Shared.NextDouble() * max; // Generates a random double between 0 and 100
        double roundedDouble = Math.Round(randomDouble, 2);
        return roundedDouble;
    }

    public static string GetSTUDENTNUMBER(int minYear, int maxYear, string dept)
    {
        // YYDPT0001
        int yy = rand.Next(minYear, maxYear + 1);
        string cyy = yy.ToString();
        int i = cyy.Length - 2;
        if (i < 0)
        {
            i = 0;
        }

        // char *dept = "CHE";
        int n = rand.Next(1, 9999);

        string os = $"{cyy.Substring(i, 2):00}{dept:000}{n:00000}";

        return os;
    }

    public static string GetDEPARTMENTCODE()
    {
        // SetTrueRandSeed();

        int i = rand.Next(0, DEPARTMENTCODE.Count);
        return DEPARTMENTCODE[i];
    }

    public static string GetSURNAME()
    {
        SetTrueRandSeed();

        int i = rand.Next(0, SURNAMES.Count);
        return SURNAMES[i];
    }

    private static void SetTrueRandSeed()
    {
        int seed = (int)DateTime.UtcNow.Ticks;
        rand = new Random(seed);
    }

    public static string GetNAME()
    {
        SetTrueRandSeed();

        int i = rand.Next(0, NAMES.Count);
        return NAMES[i];
    }

    public static string GetDOMAIN()
    {
        SetTrueRandSeed();

        int i = rand.Next(0, DOMAINS.Count);
        return DOMAINS[i];
    }

    public static string GetCOMPANYDOMAIN()
    {
        SetTrueRandSeed();

        int i = rand.Next(0, COMPANYDOMAINS.Count);
        return COMPANYDOMAINS[i];
    }

    public static string GetCOURSENAME()
    {
        SetTrueRandSeed();

        int i = rand.Next(0, COURSENAMES.Count);
        return COURSENAMES[i];
    }

    public static string GetEMAIL(string firstname, string surname, string domainName)
    {
        if (string.IsNullOrEmpty(domainName))
        {
            domainName = GetCOMPANYDOMAIN() + "." + GetDOMAIN();
        }

        // firstname.surname@company.domain
        var email = firstname + "." + surname + "@" + domainName;

        return email.ToLower();
    }

    public static char GetGENDER()
    {
        // SetTrueRandSeed();

        int i = rand.Next(0, GENDERS.Count);
        return GENDERS[i];
    }


    public static DateTime GetBIRTHDATE(int minAge, int maxAge = 100)
    {
        var UTCNow = DateTime.UtcNow;

        int addYear = GetNUMBER(minAge, 100);
        var dob = DateTime.UtcNow.AddYears(-addYear);

        return dob;
    }

    public static string GetBIRTHDATEToString(int minAge, int maxAge = 100)
    {
        var dob = GetBIRTHDATE(minAge, maxAge);

        string os = dob.ToString("yyyy-MM-dd");
        return os;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns>string e.g CHE301</returns>
    public static string GetCOURSECODE()
    {
        // SetTrueRandSeed();

        // CHE101
        var dept = GetDEPARTMENTCODE();
        var level = rand.Next(1, 5);
        var semester = rand.Next(0, 10);
        var n = rand.Next(1, 10);
        // var code = rand.Next(110,599)

        string os = GETCOURSECODE(dept, level, semester, n);
        return os;
    }

    public static string GetCOURSECODE(string dept)
    {
        // SetTrueRandSeed();

        // CHE101
        var level = rand.Next(1, 5);
        var semester = rand.Next(1, 10);
        var n = rand.Next(1, 10);
        // var code = rand.Next(110,599)

        string os = GETCOURSECODE(dept, level, semester, n);
        return os;
    }

    public static string GETCOURSECODE(string dept, int level, int semester, int n)
    {
        return $"{dept}{level}{semester}{n}";
    }

    public static string GetCOURSETITLE()
    {
        // SetTrueRandSeed();

        var v = COURSENAMES;

        // CHE101
        string text = "";

        var n = rand.Next(2, 4);
        int k = 0;
        for (int i = 0; i < n; i++)
        {
            k = rand.Next(0, v.Count);

            text += v[k];

            if (i < n - 1)
            {
                text += " ";
            }

            v.RemoveRange(k, 1);
        }

        return text;
    }

    public static int GetNUMBER(int min, int max)
    {
        // SetTrueRandSeed();
        if (min > max)
            return max;

        return rand.Next(min, max + 1);
    }

    public static T GetFrom<T>(List<T>? choices)
    {
        if (choices is null)
        {
            throw new ArgumentNullException();
        }

        // SetTrueRandSeed();
        if (choices.Count <= 0)
        {
            throw new Exception("Empty");
        }

        var i = rand.Next(0, choices.Count);

        return choices[i];
    }

    public static int GetNUMBER(List<int> choices)
    {
        if (choices is null)
        {
            throw new ArgumentNullException();
        }


        // SetTrueRandSeed();
        if (choices.Count <= 0)
        {
            throw new Exception("Empty");
        }

        var i = rand.Next(0, choices.Count);

        return choices[i];
    }

    public static double GetDECNUMBER(double min, double max, int dp = 0)
    {
        // SetTrueRandSeed();
        double range = max - min;
        double sample = rand.NextDouble();
        double scaled = sample * range + min;
        var x = Math.Round(scaled, dp);
        return x;
    }

    public static double GetDECNUMBER(List<double>? choices)
    {
        if (choices is null)
        {
            throw new ArgumentNullException();
        }

        //SetTrueRandSeed();
        if (choices.Count <= 0)
        {
            throw new ArgumentNullException();
        }

        var i = rand.Next(0, choices.Count - 1);

        return choices[i];

    }
    public static decimal GetDECNUMBER(List<decimal>? choices)
    {
        if (choices is null)
        {
            throw new ArgumentNullException();
        }
        //SetTrueRandSeed();
        if (choices.Count <= 0)
        {
            throw new ArgumentNullException();
        }

        var i = rand.Next(0, choices.Count - 1);

        return choices[i];

    }

    public static string GetPHONENUMBER(string prefix = "+234", int length = 9)
    {
        return prefix + GetDIGIT(9);
    }

    private static string GetDIGIT(int length)
    {
        List<int> num = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        string digits = string.Empty;
        for (int i = 0; i < length; i++)
        {
            digits += GetFrom(num);
        }

        return digits;
    }
}
