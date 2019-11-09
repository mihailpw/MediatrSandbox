using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace MR.Web.Infra
{
    public class LogPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<LogPreProcessor<TRequest>> _logger;

        public LogPreProcessor(ILogger<LogPreProcessor<TRequest>> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Request started: {request.GetType()}");

            return Task.CompletedTask;
        }
    }
}