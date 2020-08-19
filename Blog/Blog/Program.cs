using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var scope = host.Services.CreateScope();

            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var usrMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            ctx.Database.EnsureCreated();

            var adminRole = new IdentityRole("Admin");

            if (!ctx.Roles.Any())
            {
                roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
            }

            if (!ctx.Users.Any(u=>u.UserName == "admin"))
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };
                usrMgr.CreateAsync(adminUser,"password");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
