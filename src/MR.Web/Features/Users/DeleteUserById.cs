using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MR.Dal;

namespace MR.Web.Features.Users
{
    public static class DeleteUserById
    {
        public class Request : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly AppDbContext _appDbContext;

            public Handler(AppDbContext appDbContext)
            {
                _appDbContext = appDbContext;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _appDbContext.Users.FindAsync(request.Id);
                cancellationToken.ThrowIfCancellationRequested();
                _appDbContext.Remove(user);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}