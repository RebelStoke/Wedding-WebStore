using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WeddingApp.Core.ApplicationService;
using WeddingApp.Core.ApplicationService.HelperService;
using WeddingApp.Core.ApplicationService.ImplementedService;
using WeddingApp.Core.DomainService;
using WeddingApp.Infrastructure.SQLData;
using WeddingApp.Infrastructure.SQLData.Repositories;

namespace WeddingApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            // Add JWT based authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            services.AddCors();
            if (Environment.IsDevelopment())
            {
                services.AddDbContext<DBContext>(opt => { opt.UseSqlite("Data Source=weddingApp.db"); }
                );
            }
            else
            {
                services.AddDbContext<DBContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            }

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient<IDatabaseInitialise, DatabaseInitialise>();
            services.AddSingleton<IAuthenticationService>(new Authentication(secretBytes));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    // Initialize the database
                    var services = scope.ServiceProvider;
                    var ctx = scope.ServiceProvider.GetService<DBContext>();
                    ctx.Database.EnsureDeleted();
                    ctx.Database.EnsureCreated();
                    var dbInitializer = services.GetService<IDatabaseInitialise>();
                    dbInitializer.SeedDatabase(ctx);
                }
            }
            else
            {
                app.UseHsts();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<DBContext>();
                    ctx.Database.EnsureCreated();
                }
            }

            app.UseHttpsRedirection();
            app.UseCors(builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
