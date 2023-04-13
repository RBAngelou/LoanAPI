namespace Api.Loans.Entities
{
    public class LoanApplication
    {
        public string LoanId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }
        public decimal loanAmount { get; set; }
        public DateTime timeTrading { get; set; }
        public string countryCode { get; set; }
        public string industry { get; set; }

    }
}
