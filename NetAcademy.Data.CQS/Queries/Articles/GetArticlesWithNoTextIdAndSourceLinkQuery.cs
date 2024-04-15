using MediatR;

namespace NetAcademy.Data.CQS.Queries.Articles;

public class GetArticlesWithNoTextIdAndSourceLinkQuery 
    : IRequest<Dictionary<Guid, string>>
{
    //IT's normal to not have anything there if it make sense 
}