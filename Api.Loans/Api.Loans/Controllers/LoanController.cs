using Api.Loans.Abstract;
using Api.Loans.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Loans.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanRepository _repository;

        private readonly ILogger<LoanController> _logger;

        public LoanController(ILoanRepository repository, ILogger<LoanController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoanApplication), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<LoanApplication>> CreateDiscount([FromBody] LoanApplication newLoan)
        {
            await _repository.NewLoan(newLoan);
            return CreatedAtRoute("GetLoan", new { LoanId = newLoan.LoanId }, newLoan);
        }
    }
}