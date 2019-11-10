using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
            public Response(IEnumerable<UserDtos.Basic> users)
            {
                Users = users;
            }

            public IEnumerable<UserDtos.Basic> Users { get; }
        }

        public class Mappings : Profile
        {
            public Mappings()
            {
                CreateMap<CreateUser.Request, UserEntity>();
                CreateMap<UserEntity, UserDtos.Basic>();
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
                var users = await _appDbContext.Users
                    .TakeRange(request)
                    .ToListAsync(cancellationToken);
                return new Response(users.Select(_mapper.Map<UserDtos.Basic>));
            }
        }
    }
}