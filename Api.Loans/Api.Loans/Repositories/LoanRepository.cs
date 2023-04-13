using Api.Loans.Abstract;
using Api.Loans.ConstsAndEnums;
using Api.Loans.Entities;
using Api.Loans.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Api.Loans.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly IDistributedCache _redisCache;

        public LoanRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<LoanApplication> GetLoan(string id)
        {
            var loan = await _redisCache.GetStringAsync(id);

            if (String.IsNullOrEmpty(loan))
                return null;

            return JsonConvert.DeserializeObject<LoanApplication>(loan);
        }

        public async Task<LoanApplicationResponse> NewLoan(LoanApplication loanApplication)
        {
            LoanApplicationResponse response;
            ValidateApplication(loanApplication, out response);

            

            await _redisCache.SetStringAsync(loanApplication.LoanId, JsonConvert.SerializeObject(loanApplication));
        }

        private bool ValidateApplication(LoanApplication loanApplication, out LoanApplicationResponse response)
        {
            response = new LoanApplicationResponse();

            bool retVal = false;
            List<ValidationResult> validationResults = new List<ValidationResult>();
            if (string.IsNullOrEmpty(loanApplication.firstName))
            {
                response.decision = consDecision.UNQUALIFIED;
                validationResults.Add(new ValidationResult(rule: "nameRule", message: "No 'firstName' provided", decision: consDecision.UNQUALIFIED));

                retVal = false;
            }

            if (string.IsNullOrEmpty(loanApplication.lastName))
            {
                response.decision = consDecision.UNQUALIFIED;
                validationResults.Add(new ValidationResult(rule: "nameRule", message: "No 'lastName' provided", decision: consDecision.UNQUALIFIED));

                retVal = false;
            }

            if (string.IsNullOrEmpty(loanApplication.phoneNumber))
            {
                response.decision = consDecision.UNQUALIFIED;
                validationResults.Add(new ValidationResult(rule: "phoneNumber", message: "No 'phoneNumber' provided", decision: consDecision.UNQUALIFIED));

                retVal = false;
            }

            if (string.IsNullOrEmpty(loanApplication.emailAddress))
            {
                response.decision = consDecision.UNQUALIFIED;
                validationResults.Add(new ValidationResult(rule: "email", message: "No 'email' provided", decision: consDecision.UNQUALIFIED));

                retVal = false;
            }

            //check if phone number is valid and in Australian format (mobile/landline)
            //mobile checkup

            return retVal;
        }
    }
}
