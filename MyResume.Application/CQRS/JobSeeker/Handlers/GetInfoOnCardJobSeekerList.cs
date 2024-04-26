using MyResume.Application.Abstractions;
using MyResume.Application.CQRS.JobSeeker.Queries;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;

namespace MyResume.Application.CQRS.JobSeeker.Handlers
{
    public class GetInfoOnCardJobSeekerList : IQueryHandler<GetInfoOnCardJobSeekerOnListQuery, List<InfoOnCardJobSeekerDto>>
    {
        private readonly IJobSeekerRepository _repository;

        public GetInfoOnCardJobSeekerList(IJobSeekerRepository repository) => _repository = repository;
    
        public async Task<List<InfoOnCardJobSeekerDto>> Handle(GetInfoOnCardJobSeekerOnListQuery request, CancellationToken cancellationToken)
        {
            var jobSeekers = await _repository.GetInfoOnCardJobSeekerOnList();
            return jobSeekers;
        }
    }
}
