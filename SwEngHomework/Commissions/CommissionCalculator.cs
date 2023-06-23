using Newtonsoft.Json;

namespace SwEngHomework.Commissions
{
    public class CommissionCalculator : ICommissionCalculator
    {
        private const decimal FeeTier1 = 0.0005m; // 5 bps
        private const decimal FeeTier2 = 0.0006m; // 6 bps
        private const decimal FeeTier3 = 0.0007m; // 7 bps

        public IDictionary<string, double> CalculateCommissionsByAdvisor(string jsonInput)
        {
            IDictionary<string, double> commissions = new Dictionary<string, double>();

            dynamic data = JsonConvert.DeserializeObject(jsonInput);

            foreach (var advisor in data.advisors)
            {
                string name = advisor.name;
                string level = advisor.level;
                decimal commissionPercentage = GetCommissionPercentage(level);

                commissions[name] = CalculateCommission(data.accounts, name, commissionPercentage);
            }

            return commissions;
        }

        private decimal GetCommissionPercentage(string level)
        {
            switch (level)
            {
                case "Senior":
                    return 1.0m; // 100%
                case "Experienced":
                    return 0.5m; // 50%
                case "Junior":
                    return 0.25m; // 25%
                default:
                    throw new ArgumentException("Invalid advisor level.");
            }
        }

        private double CalculateCommission(dynamic accounts, string advisorName, decimal commissionPercentage)
        {
            decimal commission = 0m;

            foreach (var account in accounts)
            {
                if (account.advisor.ToString() == advisorName)
                {
                    decimal presentValue = Convert.ToDecimal(account.presentValue.Value);
                    decimal fee = CalculateAccountFee(presentValue);
                    commission += fee * commissionPercentage;
                }
            }

            return Math.Round((double)commission, 2);
        }

        private decimal CalculateAccountFee(decimal presentValue)
        {
            if (presentValue < 50000m)
                return presentValue * FeeTier1;
            else if (presentValue < 100000m)
                return presentValue * FeeTier2;
            else
                return presentValue * FeeTier3;
        }
    }
}
