using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Core
{
    public class Statistics
    {
        public static double? PercentRelativeError(double? experiment, double? actual)
        {
            if (actual == null || experiment is null) return null;

            return Math.Abs((experiment.Value - actual.Value) / actual.Value) * 100;
        }


        public static double PercentRelativeError(double experiment, double actual)
        {
            return Math.Abs((experiment - actual) / actual) * 100;
        }

        public static double PercentRelativeError(string? experiment, string? actual)
        {
            experiment = experiment?.Trim();
            var x = String.Compare(experiment, actual) == 0 ? 1.0 : 0;
            if (x == 0)
            {
                x = String.Compare(experiment, actual, true) == 0 ? 0.5 : 0;
            }
            return PercentRelativeError(x, 1.0);
        }

        public static IList<double> Normalize(IList<double> values)
        {
            // x' = (x-min(x)) / (max(x) - min(x))

            if (values is null || values.Count == 0)
            {
                throw new ArgumentException();
            }

            var min = values.Min();
            var max = values.Max();

            return values.Select(x => (x - min) / (max - min)).ToList();
        }

        public static double[] Normalize(params double[] values)
        {
            // x' = (x-min(x)) / (max(x) - min(x))

            if (values is null || values.Length == 0)
            {
                throw new ArgumentException();
            }

            var min = values.Min();
            var max = values.Max();

            return values.Select(x => (x - min) / (max - min)).ToArray();
        }
    }
}
