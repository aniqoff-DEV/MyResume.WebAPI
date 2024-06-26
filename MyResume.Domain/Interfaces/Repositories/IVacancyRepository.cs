﻿using MyResume.Domain.Dtos;
using MyResume.Domain.Models;

namespace MyResume.Domain.Interfaces.Repositories
{
    public interface IVacancyRepository
    {
        Task<List<InfoOnCardVacancyDto>> GetInfoOnCardList();
        Task<InfoOnPageVacancyDto> GetInfoOnPage(Guid vacancyId);
        Task<List<InfoOnCardVacancyDto>> GetInfoOnCardListByEmployerId(Guid employerId);
        Task<Guid> Create(Vacancy vacancy);
        Task Delete(Guid vacancyId);
        Task Update(
            Guid vacancyId,
            int BranchId,
            string experience,
            string employment,
            string scheduleWork,
            int salary,
            string fileName,
            byte[] file
            );
    }
}
