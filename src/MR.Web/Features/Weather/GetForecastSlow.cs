using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace MR.Web.Features.Weather
{
    public static class GetForecastSlow
    {
        public class Command : IRequest<Response>
        {
            public TimeSpan DelayTime { get; set; } = TimeSpan.FromSeconds(5);
        }

        public class Response
        {
            public WeatherForecast[] Forecasts { get; }

            public Response(WeatherForecast[] forecasts)
            {
                Forecasts = forecasts;
            }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IMediator _mediator;

            public Handler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                await Task.Delay(request.DelayTime, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();

                var response = await _mediator.Send(new GetForecast.Command(), cancellationToken);

                return new Response(response.Forecasts);
            }
        }
    }
}