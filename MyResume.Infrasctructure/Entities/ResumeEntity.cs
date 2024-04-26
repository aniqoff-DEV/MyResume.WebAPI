namespace MyResume.Infrasctructure.Entities
{
    public class ResumeEntity
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
    }
}
