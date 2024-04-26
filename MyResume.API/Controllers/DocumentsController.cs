using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyResume.API.Contracts.Requests;
using MyResume.API.Contracts.Responses;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;
using MyResume.Infrasctructure;

namespace MyResume.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("resume/create")]
        public async Task<ActionResult<Guid>> CreateResume([FromForm] DocumentRequest request)
        {
            var resume = Resume.Create(
                Guid.NewGuid(),
                request.File.FileName,
                request.File);

            if (resume.IsFailure)
                return BadRequest(resume.Error);

            Guid resumeId = await _documentService.CreateResume(resume.Value);
            return Created(Request.GetDisplayUrl(), resumeId);
        }

        [HttpGet("resume/id={resumeId}")]
        public async Task<ActionResult<DocumentResponse>> GetResume(Guid resumeId)
        {
            var resume = await _documentService.GetByResumeId(resumeId);

            if (resume is null) return NotFound();

            var response = new DocumentResponse(
                resume.Id,
                resume.FileName,
                FileConverter.ToBytes(resume.File));

            return Ok(response);
        }

        [HttpDelete("resume/id={resumeId}/delete")]
        public async Task<ActionResult> Delete(Guid resumeId)
        {
            try
            {
                await _documentService.DeleteResume(resumeId);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(Guid resumeId, [FromForm] DocumentRequest request)
        {
            try
            {
                var resume = Resume.Create(
                resumeId,
                request.File.FileName,
                request.File);

                if (resume.IsFailure)
                    return BadRequest(resume.Error);

                await _documentService.UpdateResume(resumeId, request.File);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
