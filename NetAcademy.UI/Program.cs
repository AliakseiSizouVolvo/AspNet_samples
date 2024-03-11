using Microsoft.EntityFrameworkCore;
using NetAcademy.DataBase;
using NetAcademy.Services.Abstractions;
using NetAcademy.Services.Implementation;
using NetAcademy.UI.RouteConstraints;

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


            //builder.Configuration.AddJsonFile("config.json")
            //    .AddXmlFile("config.xml")
            //    .AddIniFile("config.ini");
            
            //adding support of view associated with controllers
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

            var app = builder.Build();

            //Use Map Run 
            
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //add routing middleware
            app.UseRouting();

            app.UseAuthorization();

            
            //Convention-Based routing 
            //place where we are configuring matching of url with patterns(associating with actions)

            //app.Map("/", () => "Main Page");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            //app.MapControllerRoute("common",
            //    "{action=Index}/{controller=Home}/{page:positiveInt}/{pageSize:positiveInt}");
            //app.MapControllerRoute("dev",
            //    "{controller=Home}/{action=Index}/{page?}/{pageSize=10}");

            //app.MapControllers();// it will use routes based on action attributes
            
            //MapControllerRoute
            //MapAreaControllerRoute
            //MapControllers

            app.MapFallbackToController("Error404", "Errors");
            app.Run();
        }
    }
}
