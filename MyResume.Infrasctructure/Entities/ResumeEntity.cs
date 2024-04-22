using Microsoft.AspNetCore.Http;

namespace MyResume.Infrasctructure.Entities
{
    public class ResumeEntity
    {
        public Guid Id { get; set; }
        public Guid JobSeekerId { get; set; }
        public byte[] File { get; set; }
    }
}
