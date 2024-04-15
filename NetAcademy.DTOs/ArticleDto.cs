namespace NetAcademy.DTOs
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Text { get; set; }

        public DateTime PublicationDate { get; set; }

        public string SourceLink { get; set; }
        public string SourceName { get; set; }

    }
}
