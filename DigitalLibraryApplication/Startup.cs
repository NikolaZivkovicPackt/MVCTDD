using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalLibraryApplication.Models;
using DigitalLibraryApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalLibraryApplication
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
            var connection = Configuration.GetConnectionString("DefaultConnection");
            if (connection == null)
            {
                connection = "testingconnection";
            }

            services.AddDbContext<DigitalLibraryContext>(options =>
                options.UseSqlServer(connection));

            services.AddTransient<IAudioBookService, AudioBookService>();
            services.AddTransient<IAudioBookServiceAsync, AudioBookServiceAsync>();

            //services.AddEntityFrameworkNpgsql()
            //    .AddDbContext<DigitalLibraryContext>(
            //        options => 
            //        options.UseNpgsql(connection));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=AudioBookManual}/{action=Index}/{id?}");
            });
        }
    }
}
