using System.ServiceModel.Syndication;
using MediatR;

namespace NetAcademy.Data.CQS.Commands.Articles
{
    public class InitializeArticlesByRssDataCommand : IRequest
    {
        public IEnumerable<SyndicationItem> RssData { get; set; }
    }
}
