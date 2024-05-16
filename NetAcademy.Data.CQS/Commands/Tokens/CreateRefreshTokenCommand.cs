using MediatR;

namespace NetAcademy.Data.CQS.Commands.Tokens;

//not following CQS approaches
public class CreateRefreshTokenCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
}