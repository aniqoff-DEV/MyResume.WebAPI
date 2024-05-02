using MediatR;
using MyResume.Application.CQRS.JobSeeker.Queries;
using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Interfaces.Services;
using MyResume.Domain.Models;

namespace MyResume.Application.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IMediator _mediator;
        private readonly IVacancyRepository _repository;

        public VacancyService(IVacancyRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Guid> CreateVacancy(Vacancy vacancy)
        {
            Guid resumeId = await _repository.Create(vacancy);
            return resumeId;
        }

        public async Task Delete(Guid vacancyId)
        {
            await _repository.Delete(vacancyId);
            return;
        }

        public async Task<List<InfoOnCardVacancyDto>> GetInfoOnCardList()
        {
            var vacancy = await _mediator.Send(new GetInfoOnCardVacancyOnListQuery());
            return vacancy;
        }

        public async Task<List<InfoOnCardVacancyDto>> GetInfoOnCardListByEmployerId(Guid employerId)
        {
            var vacancy = await _mediator.Send(new GetInfoOnCardVacancyOnListByEmployerIdQuery(employerId));
            return vacancy;
        }

        public async Task<InfoOnPageVacancyDto> GetInfoOnPage(Guid vacancyId)
        {
            var vacancy = await _mediator.Send(new GetInfoOnPageVacancyByIdQuery(vacancyId));
            return vacancy;            
        }
    }
}
