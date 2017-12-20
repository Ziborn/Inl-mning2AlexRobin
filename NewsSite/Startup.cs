using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsSite.Data;
using System.Linq;

namespace NewsSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddAuthorization(options =>
            {
                // TODO: Lägg in policys...
                options.AddPolicy("HiddenNews", policy => policy.RequireClaim("UserRole", "Admin", "Subscriber", "Publisher"));
                options.AddPolicy("MinAge20", policy => policy.RequireAssertion(context =>
                {
                    var currentUser = context.User;
                    
                    if (currentUser.HasClaim(c => c.Type == "Age"))
                    {
                    var age = int.Parse(currentUser.Claims.SingleOrDefault(c => c.Type == "Age").Value);
                    if (age >= 20)
                        return true;
                    }

                    if(currentUser.HasClaim(c => c.Value == "Admin") || currentUser.HasClaim(c => c.Value == "Publisher"))
                    {
                        return true;
                    }
                    
                    // Ta reda på användarens ålder (via claim)
                    // returnera falskt eller sant
                    return false;
                }));
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();

            app.UseMvc();
        }


    }
}

