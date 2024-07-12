using CheckoutApi.Authentication;
using CheckoutApi.Authentication.Database;
using CheckoutApi.Behaviours;
using CheckoutApi.Middleware;
using CheckoutApi.Services;
using CheckoutApi.Services.Implementations;
using CheckoutApi.V1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CheckoutApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Config
            builder.Configuration.AddJsonFile("stock.json");

            // Add services to the container.
            builder.Services.AddHttpLogging(o => { });

            builder.Services.AddApiVersioning(setup =>
            {
                setup.ReportApiVersions = true;
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblyContaining<Program>();
                c.AddBehavior<LoggingPipelineBehaviour>();
            });

            builder.Services.AddScoped<ICheckoutService, CheckoutService>();
            builder.Services.AddScoped<IMessageSenderService, MessageSenderService>();
            builder.Services.AddSingleton<IStockLookupService, StockLookupService>();

            var test = builder.Configuration.GetSection(StockLookupServiceOptions.StockOptions).Get<StockLookupServiceOptions>();
            builder.Services.Configure<StockLookupServiceOptions>(builder.Configuration.GetSection(StockLookupServiceOptions.StockOptions));

            builder.Services.AddScoped<ApiKeyAuthFilter>();

            // Add a database context (in memory)
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase("AppDb"));

            // Add Auth setting up some policies using Roles & Claims
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                    policy.RequireRole(Roles.Admin));

                options.AddPolicy("CustomClaimRequirement", policy =>
                    policy.RequireClaim(Claims.CustomClaim));
            });

            // Identity /register /login ... etc other endpoints
            builder.Services.AddIdentityApiEndpoints<AppUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            //// Register ASP.NET Core Identity services
            //builder.Services.AddIdentity<AppUser, IdentityRole>()
            //        .AddEntityFrameworkStores<ApplicationDbContext>()
            //        .AddDefaultTokenProviders(); REMOVE?
            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<UserManager<AppUser>>();

            //builder.Services.AddAuthentication().AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = builder.Configuration["GoogleAuth:ClientId"] 
            //        ?? throw new Exception("Missing required secret client id"); // get from user secrets
            //    googleOptions.ClientSecret = builder.Configuration["GoogleAuth:ClientSecret"] 
            //        ?? throw new Exception("Missing required secret client secret"); // get from user secrets
            //});

            // enable Cross-Origin Resource Sharing (ajax request from another domin / react app)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllWithAuthHeader",
                    builder => builder
                        .AllowAnyOrigin() // Could only allow React app's URL
                        .WithHeaders("x-api-key") // only allow CORS with api key
                        .AllowAnyMethod());
            });

            // Setup users in background worker service
            builder.Services.AddHostedService<SetupUsersBackgroundService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //app.MapIdentityApi<IdentityUser>();

            
            app.UseHttpLogging();
            app.UseMiddleware<RequestLoggingMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowAllWithAuthHeader");
            app.UseHttpsRedirection();

            app.MapControllers();            

            app.Run();
        }
    }
}
