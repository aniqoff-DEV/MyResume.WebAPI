using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyResume.API.Contracts.Requests;
using MyResume.API.Contracts.Responses;
using MyResume.Domain.Models;
using MyResume.Domain.Services.Repositories;
using MyResume.Infrasctructure;

namespace MyResume.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
        private readonly IImageService _imageService;
        public AvatarController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Guid>> CreateAvatar([FromForm] AvatarRequest request)
        {
            var avatar = Avatar.Create(
                Guid.NewGuid(),
                request.ImageFile.FileName,
                request.ImageFile);
           
            if (avatar.IsFailure)
                return BadRequest(avatar.Error);

            Guid avatarId = await _imageService.CreateAvatar(avatar.Value);
            return Created(Request.GetDisplayUrl(), avatarId);
        }

        [HttpGet("byid={avatarId}")]
        public async Task<ActionResult<AvatarResponse>> GetAvatar(Guid avatarId)
        {
            var avatar = await _imageService.GetAvatarById(avatarId);

            if (avatar is null) return NotFound();

            var response = new AvatarResponse(
                avatar.Id,
                avatar.FileName,
                FileConverter.ToBytes(avatar.ImageFile));

            return Ok(response);
        }

        [HttpDelete("delete/byid={avatarId}")]
        public async Task<ActionResult> Delete(Guid avatarId)
        {
            try
            {
                await _imageService.DeleteAvatar(avatarId);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return NotFound();
            }           
        }

        [HttpPut]
        public async Task<ActionResult> Update(Guid avatarId, [FromForm] AvatarRequest request)
        {
            try
            {
                var avatar = Avatar.Create(
                Guid.NewGuid(),
                request.ImageFile.FileName,
                request.ImageFile);

                if (avatar.IsFailure)
                    return BadRequest(avatar.Error);

                await _imageService.UpdateAvatar(avatarId, request.ImageFile);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
