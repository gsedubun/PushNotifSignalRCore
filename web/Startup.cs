using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Models;
using core.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using signal_core.Hubs;

namespace signal_core
{
    public class Startup
    {
        private const string scheme = "signal_core";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<AppDataContext>(d =>
            {
                //d.UseMySql(Configuration.GetConnectionString("Default"), opt=>
                //{
                    
                //});
                d.UseSqlServer(Configuration.GetConnectionString("mssql"));
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, d =>
                {
                    d.LoginPath = "/account/login";
                    d.AccessDeniedPath = "/account/denied";
                    d.Cookie.Name = scheme;
                    
                }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,d=> {
                    d.RequireHttpsMetadata=false;
                    d.SaveToken=true;
                    d.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
                        ValidateIssuer=true,
                        ValidateAudience=true,
                        ValidateLifetime=true,
                        ValidateIssuerSigningKey=true,

//                        ValidIssuer= JwtSecurityKey.Issuer, //"SmartCity.Security.Bearer",
//                        ValidAudience= JwtSecurityKey.Audience, //"SmartCity.Security.Bearer",
                        IssuerSigningKey = JwtSecurityKey.Create()
                    };
                    
                });;
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
               
            // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)    
            //     .AddJwtBearer(d=> {
            //         d.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
            //             ValidateIssuer=true,
            //             ValidateAudience=true,
            //             ValidateLifetime=true,
            //             ValidateIssuerSigningKey=true,

            //             ValidIssuer="SmartCity.Security.Bearer",
            //             ValidAudience="SmartCity.Security.Bearer",
            //             IssuerSigningKey = JwtSecurityKey.Create("rahasia123")
            //         };
                    
            //     });
            services.AddSignalR();
            services.AddScoped<Unitofwork>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            //app.UseCookiePolicy()
            app.UseAuthentication();
         
            app.UseMvc(
            routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSignalR(opt => 
            {
                opt.MapHub<GreetingHub>("/hubs/greeting");
                opt.MapHub<ChatHub>("/hubs/chat");
            });
        }
    }
}
