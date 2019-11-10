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
            var requestType = request.GetType();
            _logger.LogDebug($"Request started: {requestType} ({requestType == typeof(TRequest)})");

            return Task.CompletedTask;
        }
    }
}