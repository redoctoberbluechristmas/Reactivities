using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persistence;

namespace API
{
    public class Startup
    {
        // This is a constructor the injects the configuration. Gives us access to the configuration files, appsettings.json.
        private readonly IConfiguration _config;  // Creates a private readonly field called "_config"
        public Startup(IConfiguration config)
        {
            _config = config;                      // Sets the private read-only field as being equal to the config we pass in.

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // The below container is referred to as the "dependency injection container."
        // If we want to add something that we want to use or inject into another class in our application, we will typically add it here as a service.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });
            services.AddDbContext<DataContext>(opt =>                      //AddDbContext, which comes from EntityFramework, is the object we create to call DataContext.
            {
                opt.UseSqlite(_config.GetConnectionString("DefaultConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
            }
            // Not using HTTPS for now
            //app.UseHttpsRedirection();

            // These app.X are all "middleware" in the HTTP request pipeline that perform some sort of change on incoming and ongoing requests.
            // The change can be something like routing or authorization, so setting-up endpoints.
            // This concept is specific to webapps.
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
