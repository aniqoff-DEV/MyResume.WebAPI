using MediatR;
using MyResume.Application.CQRS.JobSeeker.Queries;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;

namespace MyResume.Application.Services
{
    public class JobSeekerService : IJobSeekerService
    {
        private readonly IJobSeekerRepository _repository;
        private readonly IMediator _mediator;

        public JobSeekerService(IJobSeekerRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Guid> CreateJobSeeker(JobSeeker jobSeeker)
        {
            var newJobSeekerId = await _repository.Create(jobSeeker);
            return newJobSeekerId;
        }

        public async Task DeleteJobSeeker(Guid id)
        {
            await _repository.Delete(id);
            return;
        }

        public async Task<InfoOnCardJobSeekerDto> GetInfoOnCardById(Guid jobSeekerId)
        {
            var jobSeeker = await _mediator.Send(new GetInfoOnCardJobSeekerByIdQuery(jobSeekerId));
            return jobSeeker;
        }

        public async Task<List<InfoOnCardJobSeekerDto>> GetInfoOnCardOnList()
        {
            var jobSeekers = await _mediator.Send(new GetInfoOnCardJobSeekerOnListQuery());
            return jobSeekers;
        }

        public async Task<JobSeeker> GetJobSeekerById(Guid id)
        {
            var jobSeeker = await _repository.GetByIdRawJobSeeker(id);
            return jobSeeker;
        }

        public async Task<List<JobSeeker>> GetJobSeekers()
        {
            var jobSeekerList = await _repository.GetRawJobSeekers();
            return jobSeekerList;
        }

        public async Task UpdatePersonalDataOnJobSeeker(Guid id, string fullName, string description, string? phoneNumber, Guid avatarId, int cityId, int branchId)
        {
            await _repository.UpdateInfo(id,fullName,description,phoneNumber,avatarId,cityId,branchId);
            return;
        }
    }
}
