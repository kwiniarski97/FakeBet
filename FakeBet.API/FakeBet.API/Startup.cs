using System.Text;
using AutoMapper;
using FakeBet.API.Email;
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
using Newtonsoft.Json;
using NLog;

namespace FakeBet.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();


            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
#if DEBUG
                option.SerializerSettings.Formatting = Formatting.Indented;
#endif
            });

            // repos 
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMatchRepository, MatchRepository>();
            services.AddTransient<IBetRepository, BetRepository>();

            // services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMatchService, MatchService>();
            services.AddTransient<IBetService, BetService>();

            //dbcontext ef
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(this.Configuration["DataBase:ConnectionString"]));

            services.AddTransient<IEmailClient>(srv =>
                new EmailClient(this.Configuration["Email:Password"]));

            services.AddAutoMapper();

            services.AddTransient<IPrizeCalculator, PrizeCalculatorProportional>();

            //appsetting secret
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettingsSecret>(appSettingsSection);

            //jwt
            var appSettings = appSettingsSection.Get<AppSettingsSecret>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
            services.AddAuthorization(options => options.AddPolicy("StatusActive",
                policy => policy.RequireClaim("Status", "Active")));
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