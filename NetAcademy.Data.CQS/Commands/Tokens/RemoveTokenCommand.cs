using MediatR;

namespace NetAcademy.Data.CQS.Commands.Tokens;

public class RemoveTokenCommand : IRequest
{
    public Guid TokenId { get; set; }
}