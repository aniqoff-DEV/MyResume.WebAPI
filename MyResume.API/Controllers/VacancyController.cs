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
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;
        public VacancyController(IVacancyService vacancyService) =>_vacancyService = vacancyService;

        [HttpPost("create")]
        public async Task<ActionResult<Guid>> CreateVacancy([FromForm] VacancyRequest request)
        {
            var experience = Experience.Create(request.Experience);
            if (experience.IsFailure)
                return BadRequest(experience.Error);

            var employment = EmploymentType.Create(request.Employment);
            if (employment.IsFailure)
                return BadRequest(employment.Error);

            var schedule = Schedule.Create(request.ScheduleWork);
            if (schedule.IsFailure)
                return BadRequest(schedule.Error);

            var newVacancy = Vacancy.Create(
                Guid.NewGuid(),
                request.EmloyerId,
                request.BranchId,
            request.File.FileName,
            request.File,
            experience.Value,
            employment.Value,
            schedule.Value,
            request.Salary
            );

            if (newVacancy.IsFailure)
                return BadRequest(newVacancy.Error);

            Guid newVacancyId = await _vacancyService.CreateVacancy(newVacancy.Value);
            return Created(Request.GetDisplayUrl(), newVacancyId);
        }

        [HttpGet("all/card")]
        public async Task<ActionResult<List<InfoOnCardVacancyDto>>> GetVacancyOnCard()
        {
            var vacancy = await _vacancyService.GetInfoOnCardList();

            if (vacancy.IsNullOrEmpty())
                return NotFound();

            return Ok(vacancy);
        }

        [HttpGet("page/id={vacancyId}")]
        public async Task<ActionResult<InfoOnPageVacancyDto>> GetVacancyOnPage(Guid vacancyId)
        {
            var vacancy = await _vacancyService.GetInfoOnPage(vacancyId);

            if (vacancy is null)
                return NotFound();

            return Ok(vacancy);
        }

        [HttpGet("all/card/employerId={employerId}")]
        public async Task<ActionResult<List<InfoOnCardVacancyDto>>> GetVacancyOnCard(Guid employerId)
        {
            var vacancy = await _vacancyService.GetInfoOnCardListByEmployerId(employerId);

            if (vacancy.IsNullOrEmpty())
                return NotFound();

            return Ok(vacancy);
        }
    }
}
