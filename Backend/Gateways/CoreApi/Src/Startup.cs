using CloudYourself.Backend.Gateways.CoreApi.AzureSubscriptions;
using CloudYourself.Backend.Gateways.CoreApi.Infrastructure;
using Fancy.ResourceLinker;
using Fancy.ResourceLinker.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

namespace CloudYourself.Backend.Gateways.CoreApi
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
            services.AddSingleton<AzureSubscriptionsHubConnector>();

            services.Configure<AppServiceBaseUrlOptions>(Configuration.GetSection(AppServiceBaseUrlOptions.SectionName));

            services.AddResourceLinker(Assembly.GetExecutingAssembly());

            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.AddResourceConverter();
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CloudYourself.Backend.Gateways.CoreApi", Version = "v1" });
                c.OperationFilter<SwaggerPutPostOperationFilter>();
            });

            services.AddSignalR().AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.AddResourceConverter(false);
                options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.PayloadSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CloudYourself.Backend.Gateways.CoreApi v1"));
            }

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:4200"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AzureSubscriptionsHub>("/hubs/azure-subscriptions");
            });
        }
    }
}
