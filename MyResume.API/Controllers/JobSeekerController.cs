﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyResume.API.Contracts.Requests;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;
using MyResume.Domain.ValueObjects;

namespace MyResume.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobSeekerController : ControllerBase
    {
        private readonly IJobSeekerService _jobSeekerService;

        public JobSeekerController(IJobSeekerService jobSeekerService)
        {
            _jobSeekerService = jobSeekerService;
        }

        [HttpGet("smallinfo/id={jobSeekerId}")]
        public async Task<ActionResult<InfoOnCardJobSeekerDto>> GetInfoOnCard(Guid jobSeekerId)
        {
            var jobSeeker = await _jobSeekerService.GetInfoOnCard(jobSeekerId);

            if(jobSeeker is null) 
                return NotFound();

            return Ok(jobSeeker);
        }

        [HttpPost("create/fulldata")]
        public async Task<ActionResult<Guid>> CreateJobSeekerFullData([FromBody] JobSeekerRequest request)
        {
            var email = Email.Create(request.Email);

            if (email.IsFailure)
                return BadRequest(email.Error);

            var password = Password.Create(request.Password);

            if (password.IsFailure)
                return BadRequest(password.Error);

            var phoneNumber = PhoneNumber.Create(request.PhoneNumber!);

            if (phoneNumber.IsFailure)
                return BadRequest(phoneNumber.Error);

            #region Data
            var newJobSeeker = JobSeeker.Create(
                Guid.NewGuid(),
                request.FullName,
                request.Description,
                email.Value,
        password.Value,
        phoneNumber.Value,
        request.AvatarId,
        request.ResumeId,
        request.CityId,
        request.BranchId
                );
            #endregion
            Guid newJobSeekerId = await _jobSeekerService.CreateJobSeeker(newJobSeeker.Value);
            return Created(Request.GetDisplayUrl(), newJobSeekerId);
        }

        [HttpPost("create/startdata")]
        public async Task<ActionResult<Guid>> CreateJobSeekerStartData([FromBody] JobSeekerStartDataRequest request)
        {
            var email = Email.Create(request.Email);

            if (email.IsFailure)
                return BadRequest(email.Error);

            var password = Password.Create(request.Password);

            if (password.IsFailure)
                return BadRequest(password.Error);

            #region Data
            var newJobSeeker = JobSeeker.Create(
                Guid.NewGuid(),
                request.FullName,
                request.Description,
                email.Value,
        password.Value,
        null,
        null,
        null,
        null,
        null
                );
            #endregion
            Guid newJobSeekerId = await _jobSeekerService.CreateJobSeeker(newJobSeeker.Value);
            return Created(Request.GetDisplayUrl(), newJobSeekerId);
        }

        [HttpDelete("delete/id={jobSeekerId}")]
        public async Task<ActionResult> DeleteJobSeeker(Guid jobSeekerId)
        {
            try
            {
                await _jobSeekerService.DeleteJobSeeker(jobSeekerId);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}