using System.Linq;

namespace MR.Web.Features.Core
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> TakeRange<T>(this IQueryable<T> target, IRangedRequest rangedRequest)
        {
            return target.Skip(rangedRequest.Offset).Take(rangedRequest.Count);
        }
    }
}