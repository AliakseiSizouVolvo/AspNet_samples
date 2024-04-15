using MediatR;
using NetAcademy.DataBase.Entities;

namespace NetAcademy.Data.CQS.Queries.Articles;

public class GetArticlesWithTextQuery : IRequest<Article[]>
{
    
}