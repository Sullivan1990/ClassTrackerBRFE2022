using ClassTrackerBRFE2022.Models.TafeClassModels;
using ClassTrackerBRFE2022.Models.TeacherModels;
using ClassTrackerBRFE2022.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClassTrackerBRFE2022
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
            services.AddControllersWithViews();

            // Set up a central configuration for the HttpClient
            services.AddHttpClient("ApiClient", c =>
            {
                c.BaseAddress = new Uri(Configuration["ApiUrl"]);
                c.DefaultRequestHeaders.Clear();
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });


            // create an in memory Database for storing session content
            services.AddDistributedMemoryCache();
            // Define the session parameters
            services.AddSession(opts =>
            {
                opts.IdleTimeout = TimeSpan.FromMinutes(3);
                opts.Cookie.HttpOnly = true;
                opts.Cookie.IsEssential = true;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddScoped<IApiRequest<Teacher>, ApiRequest<Teacher>>();
            //services.AddSingleton<IApiRequest<Teacher>, ApiTestRequest<Teacher>>();
            //services.AddScoped<IApiRequest<TafeClass>, ApiRequest<TafeClass>>();

            services.AddSingleton<TestDB>();

            services.AddSingleton<IApiRequest<TafeClass>, ApiTestRequest<TafeClass>>();
            services.AddSingleton<IApiRequest<Teacher>, ApiTestRequest<Teacher>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
