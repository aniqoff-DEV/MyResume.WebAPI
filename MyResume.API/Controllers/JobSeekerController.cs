using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Services;

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
        public async Task<ActionResult<InfoOnCardJobSeekerDto>> GetInfoOnCardById(Guid jobSeekerId)
        {
            var jobSeeker = await _jobSeekerService.GetInfoOnCardById(jobSeekerId);

            if(jobSeeker is null) 
                return NotFound();

            return Ok(jobSeeker);
        }

        [HttpGet("smallinfo/all")]
        public async Task<ActionResult<List<InfoOnCardJobSeekerDto>>> GetInfoOnCardOnList()
        {
            var jobSeekers = await _jobSeekerService.GetInfoOnCardOnList();

            if (jobSeekers.IsNullOrEmpty())
                return NotFound();

            return Ok(jobSeekers);
        }

        [HttpGet("pageinfo/id={jobSeekerId}")]
        public async Task<ActionResult<InfoOnPageJobSeekerDto>> GetInfoOnPageJobSeekerById(Guid jobSeekerId)
        {
            var jobSeeker = await _jobSeekerService.GetInfoOnPageJobSeekerById(jobSeekerId);

            if (jobSeeker is null)
                return NotFound();

            return Ok(jobSeeker);
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
