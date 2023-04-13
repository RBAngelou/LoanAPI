using Api.Loans.ConstsAndEnums;

namespace Api.Loans.Models
{
    public class ValidationResult
    {
        public string rule { get; }
        public string message { get; }

        public string decision { get;  }

        public ValidationResult(string rule, string message, string decision)
        {
            this.rule = rule;
            this.message = message;
            this.decision = decision;
        }
    }
}
