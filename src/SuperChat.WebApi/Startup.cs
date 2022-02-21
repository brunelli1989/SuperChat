using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperChat.Domain.AutoMapper;
using SuperChat.Domain.Bus;
using SuperChat.Domain.Contracts;
using SuperChat.Domain.Services;
using SuperChat.ExternalServices.Contracts;
using SuperChat.ExternalServices.Services;

namespace SuperChat.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();

            services.AddSingleton(x => new ServiceBusClient(Configuration.GetConnectionString("AzureServiceBus")));
            services.AddHostedService<ServiceBusHostedService>();
            services.AddScoped<IServiceBus, ServiceBus>();
            services.AddScoped<IQuoteCalculator, QuoteCalculator>();
            services.AddScoped<IStooqExternalService, StooqExternalService>();

            services.AddAutoMapper(
                typeof(AutoMapperProfile)
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}