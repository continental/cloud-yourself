using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace CloudYourself.Backend.Gateways.CoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("config/general/cloudyourself.json");
                    config.AddJsonFile("config/general/cloudyourself.Development.json", true);
                    config.AddJsonFile("config/appspecific/backend.gateways.coreapi.json");
                    config.AddJsonFile("config/appspecific/backend.gateways.coreapi.Development.json", true);

                    IConfigurationRoot builtConfig = config.Build();

                    if (!string.IsNullOrEmpty(builtConfig.GetValue<string>("KeyVaultName")))
                    {
                        var secretClient = new SecretClient(new Uri($"https://{builtConfig["KeyVaultName"]}.vault.azure.net/"),
                                                             new DefaultAzureCredential());
                        config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
