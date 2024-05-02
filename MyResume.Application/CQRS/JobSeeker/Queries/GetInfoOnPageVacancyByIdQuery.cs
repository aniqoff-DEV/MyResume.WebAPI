using MyResume.Application.Abstractions;
using MyResume.Domain.Dtos;

namespace MyResume.Application.CQRS.JobSeeker.Queries
{
    public sealed record GetInfoOnPageVacancyByIdQuery(Guid Id) : IQuery<InfoOnPageVacancyDto>;
}
