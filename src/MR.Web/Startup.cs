using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MR.Dal;
using MR.Web.Features.Weather;
using MR.Web.Infra;

namespace MR.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(c => c.JsonSerializerOptions.WriteIndented = true);

            services.AddDbContext<AppDbContext>(c => c.UseInMemoryDatabase(nameof(AppDbContext)));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LogTimePipelineBehavior<,>));
            services.AddSingleton<IPipelineBehavior<GetForecastSlow.Request, GetForecastSlow.Response>, CacheForecastSlowPipelineBehavior>();
            services.AddMediatR(c => c.Using<CustomMediator>(), Assembly.GetAssembly(typeof(Startup)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
