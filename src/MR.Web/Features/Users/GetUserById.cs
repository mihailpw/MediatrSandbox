using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
            public Response(UserDtos.Full user)
            {
                User = user;
            }

            public UserDtos.Full User { get; }
        }

        public class Mappings : Profile
        {
            public Mappings()
            {
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
                var user = await _appDbContext.Users.FindAsync(request.Id);
                if (user == null)
                {
                    throw new NotExistsException();
                }

                return new Response(_mapper.Map<UserDtos.Full>(user));
            }
        }
    }
}