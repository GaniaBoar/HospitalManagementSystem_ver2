using Hospital.BAL.Configurations;
using HospitalManagementSystem.API.Configurations;
using HospitalManagementSystem.Common.Entities;
using HospitalManagementSystem.Common.Modal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        static readonly string _corsPolicy = "_corsPolicy";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.SwaggerConfiguration();
            services.CorsConfiguration(Configuration, _corsPolicy);

            services.AddDbContext<AppDbContext>(options =>
                 options.UseMySQL(
                     Configuration.GetConnectionString("DefaultConnection"),
                     b => b.MigrationsAssembly("HospitalManagementSystem.Common")), ServiceLifetime.Scoped);

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<AppDbContext>()
           .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

         
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "DiseaseMIS.Auth.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(3);
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton(Configuration.GetSection("JwtSettings").Get<JWTSettings>()); 

            services.DIBALResolver(Configuration);
            services.AddControllers().AddNewtonsoftJson(options => options.
              SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.ConfigureApplicationCookie(sessionConfig =>
            {
                sessionConfig.ExpireTimeSpan = TimeSpan.FromHours(24);
                sessionConfig.SlidingExpiration = true;
            });
            services.ConfigureAuth(Configuration);
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(_corsPolicy);
            app.UseRouting();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger().UseSwaggerUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
