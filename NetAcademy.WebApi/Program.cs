
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetAcademy.Data.CQS.Queries.Articles;
using NetAcademy.DataBase;
using NetAcademy.Services.Abstractions;
using NetAcademy.Services.Implementation;
using NetAcademy.UI.Mapper;
using Serilog;
using System.Reflection;
using System.Text;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetAcademy.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string myCorsPolicy = "AllowEverything";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<BookStoreDbContext>(opt =>
             opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });
            builder.Services.AddAuthorization();
            
            //add hangfire services
            builder.Services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddHangfireServer();
            
            //builder.Host.UseSerilog((context, configuration) =>
            //    configuration.ReadFrom.Configuration(context.Configuration));
            builder.Services.AddScoped<ArticleMapper>();



            builder.Services.AddScoped<IArticleService, ArticleService>();

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(
                    typeof(GetArticlesWithNoTextIdAndSourceLinkQuery).Assembly));


            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy(myCorsPolicy,
                    policy =>
                    {
                        policy
                            .WithOrigins("https://www.google.com", "https://bing.com")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Net Academy API",
                    Description = "API for Net Academy",
                    Contact = new OpenApiContact
                    {
                        Name = "Net Academy Team",
                        Email = "",
                        Url = new Uri("https://volvo.com")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT"
                    }
                });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = @"Jwt Auth header using bearer scheme like 'Bearer 1234...'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                        },
                    new List<string>()}
                });
                var xmlName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlName));
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseCors(myCorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                //Authorization = new[] { new AuthorizeFilter() }
            });
            app.MapControllers();

            app.Run();
        }
    }
}
