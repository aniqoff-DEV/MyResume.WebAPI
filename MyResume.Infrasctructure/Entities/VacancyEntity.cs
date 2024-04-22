using Microsoft.AspNetCore.Http;

namespace MyResume.Infrasctructure.Entities
{
    public class VacancyEntity
    {
        public Guid Id { get; set; }
        public Guid EmployerId { get; set; }
        public byte[] File { get; set; }
    }
}
