using MediatR;
using Microsoft.EntityFrameworkCore;
using NetAcademy.Data.CQS.Commands.Tokens;
using NetAcademy.DataBase;
using NetAcademy.DataBase.Entities;

namespace NetAcademy.Data.CQS.CommandHandlers.Tokens
{
    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, Guid>
    {
        private readonly BookStoreDbContext _dbContext;

        public CreateRefreshTokenCommandHandler
            (
                BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Guid> Handle(CreateRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var token = new Token()
            {
                UserId = command.UserId,
                TokenId = Guid.NewGuid(),
                ExpireDate = DateTime.UtcNow.AddHours(12)
            };
            await _dbContext.AddAsync(token, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return token.TokenId;
        }
    }
}
