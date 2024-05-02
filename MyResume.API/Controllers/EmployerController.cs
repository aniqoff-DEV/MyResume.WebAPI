using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyResume.API.Contracts.Requests;
using MyResume.Application.Services;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;
using MyResume.Domain.ValueObjects;

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

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateEmployer([FromBody] EmployerRequest request)
        {
            var email = Email.Create(request.Email);

            if (email.IsFailure)
                return BadRequest(email.Error);

            var password = Password.Create(request.Password);

            if (password.IsFailure)
                return BadRequest(password.Error);

            #region Data
            var newJobSeeker = Employer.Create(
                Guid.NewGuid(),
                request.CompanyName,
                email.Value,
        password.Value,
        request.AvatarId,
                request.Description,
        PhoneNumber.Create(request.PhoneNumber).Value,
        request.CityId
                );
            #endregion

            if (newJobSeeker.IsFailure)
                return BadRequest(newJobSeeker.Error);

            Guid newJobSeekerId = await _employerService.Create(newJobSeeker.Value);
            return Created(Request.GetDisplayUrl(), newJobSeekerId);
        }
    }
}
