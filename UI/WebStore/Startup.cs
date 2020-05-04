using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebStore.Clients.Employees;
using WebStore.Clients.Identity;
using WebStore.Clients.Orders;
using WebStore.Clients.Products;
using WebStore.Clients.Values;
//using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Services.InCookies;
using WebStore.Infrastructure.Services.InSQL;
using WebStore.Infrastructuse.AutoMapper;
using WebStore.Infrastructuse.Middlewere;
using WebStore.Interfaces.Api;
using WebStore.Interfaces.Services;
using WebStore.Logger;
using WebStore.Services.Products;
using WebStore.Services.Products.InCookies;

namespace WebStore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration) => this.Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile<DTOMapping>();
                opt.AddProfile<ViewModelMapping>();
            }, typeof(Startup));
            //services.AddDbContext<WebStoreDB>(opt =>
            //    opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddTransient<WebStoreDBInitializer>();

            services.AddIdentity<User, Role>()
                //.AddEntityFrameworkStores<WebStoreDB>()
                .AddDefaultTokenProviders();

            #region WebAPI Identity client store

            services
                .AddTransient<IUserStore<User>, UsersClient>()
                .AddTransient<IUserPasswordStore<User>, UsersClient>()
                .AddTransient<IUserEmailStore<User>, UsersClient>()
                .AddTransient<IUserPhoneNumberStore<User>, UsersClient>()
                .AddTransient<IUserTwoFactorStore<User>, UsersClient>()
                .AddTransient<IUserLockoutStore<User>, UsersClient>()
                .AddTransient<IUserClaimStore<User>, UsersClient>()
                .AddTransient<IUserLoginStore<User>, UsersClient>();

            services
                .AddTransient<IRoleStore<Role>, IRolesClient>();

            #endregion

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;

                //opt.User.AllowedUserNameCharacters
                opt.User.RequireUniqueEmail = false;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "WebStore";
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            //services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
            //    //@"Data Source=(local)\MSSQLLocalDB; Database=WebStore; Persist Security Info=False; MultipleActiveResultSets=True; Trusted_Connection=True;"
            //    @"Data Source=(localdb)\MSSQLLocalDB;
            //    Initial Catalog=WebStore;
            //    Integrated Security=True;
            //    Pooling=False"
            //));

            //services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<WebStoreDB>()
            //    .AddDefaultTokenProviders();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings
            //    options.Password.RequiredLength = 6;

            //    // Lockout settings
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //    options.Lockout.MaxFailedAccessAttempts = 10;
            //    options.Lockout.AllowedForNewUsers = true;

            //    // User settings
            //    options.User.RequireUniqueEmail = true;
            //});

            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.Expiration = TimeSpan.FromDays(150);
            //    options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
            //    options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
            //    options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
            //    options.SlidingExpiration = true;
            //});

            //AddTransient - каждый раз будет создаваться экземпляр сервиса
            //AddScoped - один экземпляр на область видимости 
            //AddSingleton
            //services.AddTransient<IEmployeesData, InMemoryEmplyeeData>();
            //services.AddTransient<IProductData, InMemoryProductData>();
            //services.AddSingleton<IEmployeesData, InMemoryEmplyeeData>();
            services.AddSingleton<IEmployeesData, EmployeesClient>();
            services.AddScoped<ICustomerData, SqlCustomerData>();
            //services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<IProductData, ProductsClient>();
            //services.AddScoped<ICartService, CookiesCartService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartStore, CookiesCartStore>();
            //services.AddScoped<IOrderService, SqlOrdersService>();
            services.AddScoped<IOrderService, OrdersClient>();

            services.AddScoped<IValuesService, ValuesClient>();
        }

        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            log.AddLog4Net();

            //db.Initialize();
            //db.Products.Count();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseStatusCodePages();
            app.UseStatusCodePagesWithReExecute("/Home/ErrorStatus", "?code={0}");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWelcomePage("/welcome");


            //Добавляем расширение для использования статических файлов, т.к. appsettings.json - это статический файл
            //app.UseStaticFiles();
            //app.UseAuthentication();

            app.UseMiddleware<ErrorHandling>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async context =>
                {
                    await context.Response.WriteAsync(Configuration["CustomGreetings"]);
                });

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default", 
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
