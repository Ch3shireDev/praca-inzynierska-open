using AutoMapper;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using WorldFacts.Library;
using WorldFacts.Library.Helpers;
using WorldFacts.Library.Services;

namespace WorldFacts.App;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var mapper = new MapperConfiguration(cfg=>cfg.AddProfile<AutoMapperProfile>()).CreateMapper();

        builder.Services
            .AddSingleton(mapper);

        builder.Services
            .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
            ;

        builder.Services
            .AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

        builder.Services.AddRouting(o => o.LowercaseUrls = true);

        builder.Services
            .AddRazorPages()
            .AddRazorRuntimeCompilation()
            .AddMicrosoftIdentityUI()
            ;

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder
                
                
                .Configuration
                .GetConnectionString("DefaultConnection")
                
            );
            options.UseUpperSnakeCaseNamingConvention();
        });

        builder.Services.AddScoped<IPowerBiService, PowerBiService>();
        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Services.AddScoped<INarrativeService, NarrativeService>();
        builder.Services.AddScoped<IQuestionResultService, QuestionResultService>();

        builder.Logging.AddAzureWebAppDiagnostics();

        var app = builder.Build();

        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            "default",
            "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}