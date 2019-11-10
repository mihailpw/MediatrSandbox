using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
            public string Description { get; set; }
        }

        public class Response
        {
            public Response(UserDtos.Full user)
            {
                User = user;
            }

            public UserDtos.Full User { get; }
        }

        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
            }
        }

        public class Mappings : Profile
        {
            public Mappings()
            {
                CreateMap<Request, UserEntity>();
                CreateMap<UserEntity, UserDtos.Full>();
            }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IMapper _mapper;
            private readonly AppDbContext _appDbContext;

            public Handler(IMapper mapper, AppDbContext appDbContext)
            {
                _mapper = mapper;
                _appDbContext = appDbContext;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<UserEntity>(request);

                await _appDbContext.AddAsync(user, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return new Response(_mapper.Map<UserDtos.Full>(user));
            }
        }
    }
}