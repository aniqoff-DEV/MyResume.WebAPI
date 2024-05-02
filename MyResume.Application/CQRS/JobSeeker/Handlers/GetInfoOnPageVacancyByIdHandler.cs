using MyResume.Application.Abstractions;
using MyResume.Application.CQRS.JobSeeker.Queries;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;

namespace MyResume.Application.CQRS.JobSeeker.Handlers
{
    public class GetInfoOnPageVacancyByIdHandler : IQueryHandler<GetInfoOnPageVacancyByIdQuery, InfoOnPageVacancyDto>
    {
        private readonly IVacancyRepository _vacancyRepository;

        public GetInfoOnPageVacancyByIdHandler(IVacancyRepository vacancyRepository) => _vacancyRepository = vacancyRepository;

        public async Task<InfoOnPageVacancyDto> Handle(GetInfoOnPageVacancyByIdQuery request, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyRepository.GetInfoOnPage(request.Id);
            return vacancy;
        }
    }
}
