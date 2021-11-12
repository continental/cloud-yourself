using CloudYourself.Backend.AppServices.Azure.Infrastructure;
using CloudYourself.Backend.AppServices.Azure.Services;
using Fancy.ResourceLinker.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace CloudYourself.Backend.AppServices.Azure
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
            // Add database services
            string dbConnectionString = Configuration.GetConnectionString("DbConnection");
            services.AddDbContext<AzureDbContext>(options => options.UseSqlServer(dbConnectionString), optionsLifetime: ServiceLifetime.Singleton);

            // Add system services
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.AddResourceConverter();
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CloudYourself.Backend.AppServices.AzureSubscriptions", Version = "v1" });
            });

            // Add app services
            services.AddSingleton<SubscriptionService>();
            services.AddSingleton<DeploymentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AzureDbContext dbContext)
        {
            // Migrate database to latest version
            dbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CloudYourself.Backend.AppServices.Azure"));
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
