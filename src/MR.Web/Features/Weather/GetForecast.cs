using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MR.Web.Features.Weather
{
    public static class GetForecast
    {
        public class Request : IRequest<Response>
        {
            
        }

        public class Response
        {
            public WeatherForecast[] Forecasts { get; }

            public Response(WeatherForecast[] forecasts)
            {
                Forecasts = forecasts;
            }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private static readonly string[] Summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            private readonly ILogger<Handler> _logger;

            public Handler(ILogger<Handler> logger)
            {
                _logger = logger;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                await Task.CompletedTask;

                var rng = new Random();

                return new Response(
                    Enumerable.Range(1, 5)
                        .Select(
                            index => new WeatherForecast
                            {
                                Date = DateTime.Now.AddDays(index),
                                TemperatureC = rng.Next(-20, 55),
                                Summary = Summaries[rng.Next(Summaries.Length)]
                            })
                        .ToArray());
            }
        }
    }
}