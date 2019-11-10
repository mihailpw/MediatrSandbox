using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace MR.Web.Infra
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidatorFactory _validatorFactory;

        public ValidationPipelineBehavior(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validator = _validatorFactory.GetValidator<TRequest>();
            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
            }

            return await next().ConfigureAwait(false);
        }
    }
}