using MediatR;

namespace MR.Web.Features.Core
{
    public interface IRangedRequest
    {
        int Offset { get; set; }
        int Count { get; set; }
    }


    public class RangedRequest<TResponse> : IRequest<TResponse>, IRangedRequest
    {
        public int Offset { get; set; }
        public int Count { get; set; } = 20;
    }
}