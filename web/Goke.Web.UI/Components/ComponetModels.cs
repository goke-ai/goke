using Goke.Web.Shared.Models;

namespace Goke.Razor.Components
{
    public class Functions
    {
        

        public static List<int>[] RandomSplit(int total, int part)
        {
            List<int> choices = Enumerable.Range(0, total).ToList();

            List<int>[]result = new List<int>[part];
            for (int i = 0; i < part; i++)
            {
                result[i] = [];
            }

            int count = 1;
            while (choices.Count > 0)
            {
                int i = Random.Shared.Next(choices.Count);
                int k = choices[i];

                int j = count % part;
                result[j].Add(k);

                choices.Remove(k);
                ++count;
            }

            Random.Shared.Shuffle(result);

            return result;
        }

        public static List<T>[] RandomSplit<T>(IEnumerable<T> items, int part)
        {
            List<int> choices = Enumerable.Range(0, items.Count()).ToList();

            List<T>[] result = new List<T>[part];
            for (int i = 0; i < part; i++)
            {
                result[i] = [];
            }

            int count = 1;
            while (choices.Count > 0)
            {
                int i = Random.Shared.Next(choices.Count);
                int k = choices[i];

                int j = count % part;
                result[j].Add(items.ElementAt(k));

                choices.Remove(k);
                ++count;
            }

            Random.Shared.Shuffle(result);

            return result;
        }
    }

    public class ToolbarItem
    {
        public string? Image { get; set; }
        public string? Title { get; set; }
        public ApparatusType ApparatusItem { get; set; }
    }
}
