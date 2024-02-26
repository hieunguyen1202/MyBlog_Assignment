using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MyBlog.Areas.Admin.Data;
using MyBlog.Areas.Admin.Data.Repository;
using MyBlog.Data.Repository;
namespace MyBlog
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public Startup()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var stringConnectdb = Configuration.GetConnectionString("MyBlogDB");
            services.AddDbContext<MyBlogContext>(options => options.UseSqlServer(stringConnectdb), ServiceLifetime.Scoped);
            services.AddControllersWithViews();
            services.AddSession();
            //services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRange.All }));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddAuthentication("CookieAuthentication")
                .AddCookie("CookieAuthentication", options =>
                {
                    options.Cookie.Name = "UserLoginCookie";
                    options.ExpireTimeSpan = TimeSpan.FromDays(150);
                    options.LoginPath = "/dang-nhap.html";
                    options.LogoutPath = "/dang-xuat.html";
                    options.AccessDeniedPath = "/not-found.html";
                });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.SlidingExpiration = true;
            });
            //services.AddScoped<MyBlogContext>();
            services.AddScoped<IPostRespository, PostRepository>();
            services.AddScoped<ICategoriesRespository, CategoriesRespository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession(); //<= login
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
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //who are you?
            app.UseAuthentication();
            //are you allowed?
            app.UseAuthorization();
            //app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
              endpoints.MapControllerRoute(
            name: "Admin",
            pattern: "{area:exists}/{controller=Accounts}/{action=Login}");
            });
        }
    }
}
