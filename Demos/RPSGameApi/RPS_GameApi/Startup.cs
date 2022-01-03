using Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace RPS_GameApi
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
            //This is where you will add
            //// DI (Dependency Injections)
            ///Three types of lifetimes for objects:
            ///Scoped = there is an instance of the object created for every call cycle in with the object is needed.
            ///Singleton = one instance of the object created that lasts the lifetime of the compiliation.
            ///Transient = one object created and destroyed every time it's needed.
            services.AddScoped<IDataBaseAccess, DataBaseAccess>();
            services.AddScoped<IGamePlayLogic, GamePlayLogic>();
            services.AddScoped<IMapper, Mapper>();

            //// CORS

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RPS_GameApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RPS_GameApi v1"));
            }

            app.UseHttpsRedirection();
            //app.UseCores()
            app.UseRouting();

            app.UseAuthorization();

            // here we are using a Lambda Expression...
            // AKA arrow function (JS),
            // Predicate(A method sent into another method.)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
