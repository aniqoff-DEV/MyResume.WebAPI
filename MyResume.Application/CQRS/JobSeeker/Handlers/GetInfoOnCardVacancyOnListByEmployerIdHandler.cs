using MyResume.Application.Abstractions;
using MyResume.Application.CQRS.JobSeeker.Queries;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;

namespace MyResume.Application.CQRS.JobSeeker.Handlers
{
    public class GetInfoOnCardVacancyOnListByEmployerIdHandler : IQueryHandler<GetInfoOnCardVacancyOnListByEmployerIdQuery, List<InfoOnCardVacancyDto>>
    {
        private readonly IVacancyRepository _vacancyRepository;

        public GetInfoOnCardVacancyOnListByEmployerIdHandler(IVacancyRepository vacancyRepository) => _vacancyRepository = vacancyRepository;

        public async Task<List<InfoOnCardVacancyDto>> Handle(GetInfoOnCardVacancyOnListByEmployerIdQuery request, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyRepository.GetInfoOnCardListByEmployerId(request.employerId);
            return vacancy;
        }
    }
}
