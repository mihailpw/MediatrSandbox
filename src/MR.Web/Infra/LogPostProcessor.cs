using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace MR.Web.Infra
{
    public class LogPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly ILogger<LogPostProcessor<TRequest, TResponse>> _logger;

        public LogPostProcessor(ILogger<LogPostProcessor<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Request ended: {request.GetType()} with response: {response.GetType()}");

            return Task.CompletedTask;
        }
    }
}