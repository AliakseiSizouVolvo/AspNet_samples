using MediatR;
using NetAcademy.DataBase.Entities;

namespace NetAcademy.Data.CQS.Queries.Tokens;

public class GetUserByRefreshTokenQuery : IRequest<User>
{
    public Guid TokenId { get; set; }
}