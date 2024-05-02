using MyResume.Domain.Dtos;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Services
{
    public interface IVacancyService
    {
        Task<Guid> CreateVacancy(Vacancy vacancy);
        Task Delete(Guid vacancyId);
        Task<List<InfoOnCardVacancyDto>> GetInfoOnCardList();
        Task<InfoOnPageVacancyDto> GetInfoOnPage(Guid vacancyId);
        Task<List<InfoOnCardVacancyDto>> GetInfoOnCardListByEmployerId(Guid employerId);
    }
}
