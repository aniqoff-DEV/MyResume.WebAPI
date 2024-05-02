using MyResume.Application.Abstractions;
using MyResume.Application.CQRS.JobSeeker.Queries;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;

namespace MyResume.Application.CQRS.JobSeeker.Handlers
{
    public class GetInfoOnCardVacancyListHandler : IQueryHandler<GetInfoOnCardVacancyOnListQuery, List<InfoOnCardVacancyDto>>
    {
        private readonly IVacancyRepository _vacancyRepository;

        public GetInfoOnCardVacancyListHandler(IVacancyRepository vacancyRepository) => _vacancyRepository = vacancyRepository;

        public async Task<List<InfoOnCardVacancyDto>> Handle(GetInfoOnCardVacancyOnListQuery request, CancellationToken cancellationToken)
        {
            var vacancy = await _vacancyRepository.GetInfoOnCardList();
            return vacancy;
        }
    }
}
