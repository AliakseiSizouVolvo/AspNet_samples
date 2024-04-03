using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NetAcademy.DataBase;
using NetAcademy.Services.Abstractions;
using NetAcademy.Services.Implementation;
using NetAcademy.UI.Filters;
using NetAcademy.UI.RouteConstraints;
using Serilog;

namespace NetAcademy.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BookStoreDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddRouting(opt => opt.ConstraintMap.Add("positiveInt", typeof(PositiveIntConstraint)));

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/User/Login";
                    opt.LogoutPath = "/User/Logout";
                    opt.AccessDeniedPath = "/User/AccessDenied";
                });
            builder.Services.AddAuthorization(
                opts =>
                {
                    opts.AddPolicy("OnlyFor18+", policy =>
                    {
                        policy.RequireClaim("Age", "18+");
                    });
                });

            //adding support of view associated with controllers
            builder.Services.AddMvc(opt =>
            {
                //opt.Filters.Add(typeof(WhitespaceRemoverAttribute));
                //opt.Filters.Add(new WhitespaceRemoverAttribute());
                opt.Filters.Add<DateTimeExecutionFilter>();
            });
            builder.Services.AddControllersWithViews();

            //builder.Services.Configure<RouteOptions>(opt =>
            //{
            //    opt.ConstraintMap.Add("positiveInt", typeof(PositiveIntConstraint));
            //    opt.ConstraintMap.Add("secretCode", typeof(SecretCodeConstraint));
            //});

            builder.Services.AddScoped<ICustomerOrderService, CustomerOrderService>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ITest1Service, Test1Service>();
            builder.Services.AddScoped<ITest2Service, Test2Service>();
            builder.Services.AddTransient<ITransientService, TransientService>();
            builder.Services.AddScoped<IScopedService, ScopedService>();
            builder.Services.AddSingleton<ISingletonService, SingletonService>();
            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            //builder.Services.AddTransient<DIActionFilterAttribute>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            //add routing middleware
            app.UseRouting();


            app.UseAuthentication(); //be sure that users are who they say -> set user to be a user 

            app.UseAuthorization(); // grant permission/access/etc


            //Convention-Based routing 
            //place where we are configuring matching of url with patterns(associating with actions)

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.MapFallbackToController("Error404", "Errors");

            app.UsePasswordTracker();
            app.Run();
        }
    }
}
