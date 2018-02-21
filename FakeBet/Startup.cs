using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FakeBet.Repository;
using FakeBet.Repository.Implementations;
using FakeBet.Repository.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FakeBet
{
    using AutoMapper;

    using FakeBet.DTO;
    using FakeBet.Models;
    using FakeBet.Services.Implementations;
    using FakeBet.Services.Interfaces;

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
            services.AddMvc();

            // repos 
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMatchRepository, MatchRepository>();
            services.AddTransient<IVoteRepository, VoteRepository>();

            // services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMatchService, MatchService>();
            services.AddTransient<IVoteService, VoteService>();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(this.Configuration["ConnectionString"]));

            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions { HotModuleReplacement = true });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(
                routes =>
                    {
                        routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");

                        // routes.MapSpaFallbackRoute(
                        // name: "spa-fallback",
                        // defaults: new {controller = "Home", action = "Index"});
                    });
        }
    }
}