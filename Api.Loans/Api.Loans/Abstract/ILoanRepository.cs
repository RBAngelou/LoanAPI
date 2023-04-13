using Api.Loans.Entities;

namespace Api.Loans.Abstract
{
    public interface ILoanRepository
    {
        public Task<LoanApplication> GetLoan(string id);

        public Task NewLoan(LoanApplication loanApplication);

    }
}
