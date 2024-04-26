using MyResume.Application.Abstractions;
using MyResume.Application.CQRS.JobSeeker.Queries;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;

namespace MyResume.Application.CQRS.JobSeeker.Handlers
{
    public class GetInfoOnCardJobSeekerByIdHandler : IQueryHandler<GetInfoOnCardJobSeekerByIdQuery, InfoOnCardJobSeekerDto>
    {
        private readonly IJobSeekerRepository _repository;

        public GetInfoOnCardJobSeekerByIdHandler(IJobSeekerRepository repository) => _repository = repository;

        public async Task<InfoOnCardJobSeekerDto> Handle(GetInfoOnCardJobSeekerByIdQuery request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _repository.GetInfoOnCard(request.Id);

            //if (note is null)
            //    throw new NoteNotFoundException();

            return jobSeeker;
        }
    }
}
