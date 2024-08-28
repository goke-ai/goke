using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Core
{
    public class Statistics
    {
        // Method to calculate the relative error
        public static List<double> RelativeError(List<double> actual, List<double> predicted)
        {
            if (actual == null || predicted == null || actual.Count == 0 || predicted.Count == 0 || actual.Count != predicted.Count)
            {
                throw new ArgumentException("The lists cannot be null, empty, and must have the same number of elements.");
            }

            return actual.Zip(predicted, (a, p) => Math.Abs((a - p) / a)).ToList();
        }

        // Method to calculate the relative error
        public static double RelativeError(double actual, double predicted)
        {
            return Math.Abs((actual - predicted) / actual);
        }

        public static double? PercentRelativeError(double? actual, double? predicted)
        {
            if (actual == null || predicted is null) 
                return null;

            return RelativeError(actual.Value, predicted.Value) * 100.0;
        }

        public static double PercentRelativeError(double actual, double predicted)
        {
            return RelativeError(actual, predicted) * 100.0;
        }

        public static double PercentRelativeError(string? actual, string? predicted)
        {
            predicted = predicted?.Trim();
            var x = String.Compare(predicted, actual) == 0 ? 1.0 : 0;
            if (x == 0)
            {
                x = String.Compare(predicted, actual, true) == 0 ? 0.5 : 0;
            }
            return PercentRelativeError(1.0, x);
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
