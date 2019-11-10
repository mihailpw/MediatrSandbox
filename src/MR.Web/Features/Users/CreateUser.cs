using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MR.Dal;
using MR.Web.Models;

namespace MR.Web.Features.Users
{
    public static class CreateUser
    {
        public class Request : IRequest<Response>
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public class Response
        {
            public Response(UserEntity user)
            {
                User = user;
            }

            public UserEntity User { get; }
        }

        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
            }
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
                var user = new UserEntity
                {
                    Name = request.Name,
                    Email = request.Email,
                };

                await _appDbContext.AddAsync(user, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return new Response(user);
            }
        }
    }
}