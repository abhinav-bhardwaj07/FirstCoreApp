using FirstCoreApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstCoreApp.ModelsNew;

namespace FirstCoreApp
{
    public class Startup
    {
        private IConfiguration _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<FirstCoreDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseStaticFiles();
            app.UseRouting();

            //conventional routing
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}");
               //endpoints.MapDefaultControllerRoute();
            });

            //app.Run(async (context) =>
            //{
            //    //throw new Exception("Testing exception from dev environment");
            //    await context.Response.WriteAsync("Hello world !!");
            //});



        }
    }
}

// Middleware

//nuget => is the marketpalace of microsoft, from where you can download and library or package in the form of packages
//logging => to log messages or errors or debug info and details 
// wwwroot folder
// defualt.html, default.htm, index.html,index.htm

///MVC => Model View Controller, DI