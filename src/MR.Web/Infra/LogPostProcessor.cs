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
            var requestType = request.GetType();
            var responseType = response.GetType();
            _logger.LogDebug($"Request ended: {requestType} ({requestType == typeof(TRequest)}) with response: {responseType} ({responseType == typeof(TResponse)})");

            return Task.CompletedTask;
        }
    }
}