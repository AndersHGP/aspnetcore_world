using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApplication1.Model;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Startup
    {
        private IHostingEnvironment _environment;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment environment)
        {
            _environment = environment;

            var builder = new ConfigurationBuilder()
            .SetBasePath(_environment.ContentRootPath)
            .AddJsonFile("config.json")
            .AddEnvironmentVariables();

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            services.AddScoped<IMailService, DebugMailService>();

            if (_environment.IsDevelopment() || _environment.IsStaging())
            {
                services.AddScoped<IMailService, DebugMailService>();
            }
            else
            {
                throw new NotImplementedException(); //TODO: Should be implemented before production (Anders)
            }            if (_environment.IsDevelopment() || _environment.IsStaging())
            {
                services.AddScoped<IMailService, DebugMailService>();
            }
            else
            {
                throw new NotImplementedException(); //TODO: Should be implemented before production (Anders)
            }

            services.AddDbContext<WorldContext>();
            services.AddScoped<IWorldRepository, WorldRepository>();
            services.AddTransient<WorldContextSeedData>();
            services.AddMvc();
            services.AddLogging();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, WorldContextSeedData seeder)
        {
            loggerFactory.AddConsole();
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }
            
            app.UseStaticFiles();
            app.UseMvc(config => {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action ="Index"}
                );

            });
            
            seeder.EnsureSeedData().Wait();
        }
    }
}
