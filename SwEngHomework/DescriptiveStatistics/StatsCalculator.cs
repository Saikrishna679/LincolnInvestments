namespace SwEngHomework.DescriptiveStatistics
{
    public class StatsCalculator : IStatsCalculator
    {
        public Stats Calculate(string semicolonDelimitedContributions)
        {
            Stats stats = new Stats();

            List<double> contributions = ParseContributions(semicolonDelimitedContributions);

            if (contributions.Count == 0)
                return stats;

            stats.Average = CalculateAverage(contributions);
            stats.Median = CalculateMedian(contributions);
            stats.Range = CalculateRange(contributions);

            return stats;
        }

        private List<double> ParseContributions(string semicolonDelimitedContributions)
        {
            List<double> contributions = new List<double>();

            string[] tokens = semicolonDelimitedContributions.Split(';');

            foreach (string token in tokens)
            {
                if (double.TryParse(token.Trim().Replace("$", "").Replace(",", ""), out double contribution))
                {
                    contributions.Add(contribution);
                }
            }

            return contributions;
        }

        private double CalculateAverage(List<double> contributions)
        {
            return Math.Round(contributions.Average(), 2);
        }

        private double CalculateMedian(List<double> contributions)
        {
            List<double> sortedContributions = contributions.OrderBy(c => c).ToList();
            int count = sortedContributions.Count;
            int mid = count / 2;

            if (count % 2 == 0)
            {
                return Math.Round((sortedContributions[mid - 1] + sortedContributions[mid]) / 2, 2);
            }
            else
            {
                return Math.Round(sortedContributions[mid], 2);
            }
        }

        private double CalculateRange(List<double> contributions)
        {
            double max = contributions.Max();
            double min = contributions.Min();
            return max - min;
        }

    }
}
