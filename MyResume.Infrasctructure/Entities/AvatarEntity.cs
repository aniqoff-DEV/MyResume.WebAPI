namespace MyResume.Infrasctructure.Entities
{
    public class AvatarEntity
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] ImageFile { get; set; }
    }
}
