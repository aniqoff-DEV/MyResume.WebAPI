namespace MyResume.Infrasctructure.Entities
{
    public class JobSeekerFeedBackEntity
    {
        public Guid Id { get; set; }
        public Guid JobSeekerId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberStars { get; set; }
    }
}
