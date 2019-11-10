using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using MR.Web.Features.Weather;

namespace MR.Web.Infra
{
    public class CacheForecastSlowPipelineBehavior : IPipelineBehavior<GetForecastSlow.Request, GetForecastSlow.Response>
    {
        private readonly ILogger<CacheForecastSlowPipelineBehavior> _logger;

        private DateTime _cachedTime;
        private GetForecastSlow.Response _cachedValue;

        public CacheForecastSlowPipelineBehavior(ILogger<CacheForecastSlowPipelineBehavior> logger)
        {
            _logger = logger;
        }

        public async Task<GetForecastSlow.Response> Handle(
            GetForecastSlow.Request request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<GetForecastSlow.Response> next)
        {
            if (_cachedValue == null
                || _cachedTime < DateTime.UtcNow.AddSeconds(-5))
            {
                _cachedValue = await next();
                _cachedTime = DateTime.UtcNow;
                _logger.LogDebug($"Updated {typeof(GetForecastSlow)} cache.");
            }
            else
            {
                _logger.LogWarning($"Got {typeof(GetForecastSlow)} from cache.");
            }

            return _cachedValue;
        }
    }
}