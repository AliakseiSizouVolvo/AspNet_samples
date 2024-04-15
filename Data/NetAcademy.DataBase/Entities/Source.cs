namespace NetAcademy.DataBase.Entities;

public class Source
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string RssUrl { get; set; }
    public string OriginUrl { get; set; }
    
    public List<Article> Articles { get; set; }
}