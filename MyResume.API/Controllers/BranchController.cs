using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyResume.API.Contracts.Requests;
using MyResume.API.Contracts.Responses;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;

namespace MyResume.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _service;

        public BranchController(IBranchService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BranchResponse>>> GetBranches()
        {
            var branches = await _service
                .GetBranches();

            var response = branches.Select(c =>
                new BranchResponse(c.Id, c.Name));

            if (response is null)
                return BadRequest();
            return Ok(response);
        }

        [HttpGet("byid={branchId}")]
        public async Task<ActionResult<BranchResponse>> GetContry(int branchId)
        {
            var branch = await _service.GetBranchById(branchId);

            if (branch is null) return NotFound();

            var response = new BranchResponse(branch.Id, branch.Name);

            return Ok(response);
        }

        [HttpPost("insert")]
        public async Task<ActionResult<BranchResponse>> InsertContry([FromBody] BranchRequest branchRequest)
        {
            var newBranch = Branch.Create(0, branchRequest.Name);

            if (!newBranch.IsSuccess)
                return BadRequest(newBranch.Error);

            var branchId = await _service.CreateBranch(newBranch.Value);

            if (branchId <= 0)
                return BadRequest();

            return Created(Request.GetDisplayUrl(), new BranchResponse(branchId, branchRequest.Name));
        }

        [HttpDelete("delete/byid={branchId}")]
        public async Task<ActionResult> Delete(int branchId)
        {
            try
            {
                await _service.DeleteBranch(branchId);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return NotFound();
            }            
        }
    }
}
