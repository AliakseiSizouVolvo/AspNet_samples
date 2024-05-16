using MediatR;
using Microsoft.EntityFrameworkCore;
using NetAcademy.Data.CQS.Commands.Tokens;
using NetAcademy.DataBase;

namespace NetAcademy.Data.CQS.CommandHandlers.Tokens
{
    public class RemoveTokenCommandHandler : IRequestHandler<RemoveTokenCommand>
    {
        private readonly BookStoreDbContext _dbContext;

        public RemoveTokenCommandHandler
            (
                BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task Handle(RemoveTokenCommand command, CancellationToken cancellationToken)
        {
            var token = await _dbContext.Tokens
                .FirstOrDefaultAsync(article => article.TokenId.Equals(command.TokenId), cancellationToken);
            _dbContext.Remove(token);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
