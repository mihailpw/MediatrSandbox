using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MR.Dal;
using MR.Web.Features.Core;
using MR.Web.Models;

namespace MR.Web.Features.Users
{
    public static class GetUserById
    {
        public class Request : IRequest<Response>
        {
            public int Id { get; set; }
        }

        public class Response
        {
            public Response(UserEntity user)
            {
                User = user;
            }

            public UserEntity User { get; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly AppDbContext _appDbContext;

            public Handler(AppDbContext appDbContext)
            {
                _appDbContext = appDbContext;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _appDbContext.Users.FindAsync(request.Id);
                if (user == null)
                {
                    throw new NotExistsException();
                }

                return new Response(user);
            }
        }
    }
}