using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MR.Web.Infra
{
    public class LogTimePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LogTimePipelineBehavior<TRequest, TResponse>> _logger;

        public LogTimePipelineBehavior(ILogger<LogTimePipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var stopWatch = Stopwatch.StartNew();
            var result = await next().ConfigureAwait(false);
            stopWatch.Stop();

            _logger.LogDebug($"Request finished in: {stopWatch.Elapsed:c}");

            return result;
        }
    }
}