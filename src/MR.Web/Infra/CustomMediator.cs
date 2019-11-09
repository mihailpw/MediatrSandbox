using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;

namespace MR.Web.Infra
{
    public class CustomMediator : Mediator
    {
        public CustomMediator(ServiceFactory serviceFactory) : base(serviceFactory)
        {
        }

        protected override async Task PublishCore(IEnumerable<Func<Task>> allHandlers)
        {
            // await Task.WhenAll(allHandlers.Select(h => h())).ConfigureAwait(false);

            await base.PublishCore(allHandlers);
        }
    }
}