using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Services;

namespace MyResume.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService _employerService;

        public EmployerController(IEmployerService employerService) => _employerService = employerService;

        [HttpGet("company/id={employerId}")]
        public async Task<ActionResult<EmployerDto>> GetCompanyCard(Guid employerId)
        {
            var companyCard = await _employerService.GetCompanyCardById(employerId);

            if (companyCard is null)
                return NotFound();

            return Ok(companyCard);
        }

        [HttpGet("company")]
        public async Task<ActionResult<EmployerDto>> GetCompanyCards()
        {
            var companyCards = await _employerService.GetCompanyCards();

            if (companyCards.IsNullOrEmpty())
                return NotFound();

            return Ok(companyCards);
        }
    }
}
