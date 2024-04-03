namespace NetAcademy.UI.Models;

public class ArticleModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Text { get; set; }

    public DateTime PublicationDate { get; set; }

    //image(preview)

    public string SourceLink { get; set; }
}