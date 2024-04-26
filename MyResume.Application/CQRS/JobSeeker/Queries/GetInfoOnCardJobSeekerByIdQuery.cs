using MyResume.Application.Abstractions;
using MyResume.Domain.Dtos;

namespace MyResume.Application.CQRS.JobSeeker.Queries
{
    public sealed record GetInfoOnCardJobSeekerByIdQuery(Guid Id) : IQuery<InfoOnCardJobSeekerDto>;
}
