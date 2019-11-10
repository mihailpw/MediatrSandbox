using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MR.Dal;
using MR.Web.Features.Core;
using MR.Web.Models;

namespace MR.Web.Features.Users
{
    public static class GetAllUsers
    {
        public class Request : RangedRequest<Response>
        {
        }

        public class Response
        {
            public Response(IEnumerable<UserEntity> users)
            {
                Users = users;
            }

            public IEnumerable<UserEntity> Users { get; }
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
                var users = await _appDbContext.Users
                    .TakeRange(request)
                    .ToListAsync(cancellationToken);
                return new Response(users);
            }
        }
    }
}