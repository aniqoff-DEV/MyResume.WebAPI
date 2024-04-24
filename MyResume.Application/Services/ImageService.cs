using Microsoft.AspNetCore.Http;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;

namespace MyResume.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<Guid> CreateAvatar(Avatar avatar)
        {
            Guid avatarId = await _imageRepository.CreateAvatar(avatar);
            return avatarId;
        }

        public async Task DeleteAvatar(Guid id)
        {
            await _imageRepository.DeleteAvatar(id);
            return;
        }

        public async Task<Avatar> GetAvatarById(Guid id)
        {
            var avatar = await _imageRepository.GetByIdAvatar(id);
            return avatar;
        }

        public async Task UpdateAvatar(Guid id, IFormFile imageFile)
        {
            await _imageRepository.UpdateAvatar(id,imageFile);
            return;
        }
    }
}
