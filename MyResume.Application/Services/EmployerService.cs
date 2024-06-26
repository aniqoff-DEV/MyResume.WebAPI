﻿using MyResume.Domain.Dtos;
using MyResume.Domain.Interfaces.Repositories;
using MyResume.Domain.Interfaces.Services;

namespace MyResume.Application.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository _repository;

        public EmployerService(IEmployerRepository repository) => _repository = repository;

        public async Task<EmployerDto> GetCompanyCardById(Guid employerId)
        {
            var companyCard = await _repository.GetCompanyCardById(employerId);
            return companyCard;
        }

        public async Task<List<EmployerDto>> GetCompanyCards()
        {
            var companyCards = await _repository.GetCompanyCards();
            return companyCards;
        }
    }
}
