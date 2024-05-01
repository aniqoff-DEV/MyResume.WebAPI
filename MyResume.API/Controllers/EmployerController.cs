using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("id={employerId}")]
        public async Task<ActionResult<EmployerDto>> GetEmployer(Guid employerId)
        {
            var employer = await _employerService.GetEmployerById(employerId);

            if (employer is null)
                return NotFound();

            return Ok(employer);
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
