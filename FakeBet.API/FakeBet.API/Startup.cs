using System.Text;
using AutoMapper;
using FakeBet.API.Helpers;
using FakeBet.API.Repository;
using FakeBet.API.Repository.Implementations;
using FakeBet.API.Repository.Interfaces;
using FakeBet.API.Services.Implementations;
using FakeBet.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FakeBet.API
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
            services.AddMvc();
            // repos 
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMatchRepository, MatchRepository>();
            services.AddTransient<IVoteRepository, VoteRepository>();

            // services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMatchService, MatchService>();
            services.AddTransient<IVoteService, VoteService>();

            //dbcontext ef
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(this.Configuration["DataBase:ConnectionString"]));


            services.AddAutoMapper();
            //appsetting secret
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettingsSecret>(appSettingsSection);

            //jwt
            var appSettings = appSettingsSection.Get<AppSettingsSecret>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //cors
            app.UseCors(x =>
            {
                x.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}