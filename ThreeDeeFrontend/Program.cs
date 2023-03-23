using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using ThreeDeeFrontend.Components;
using ThreeDeeFrontend.ViewModels;
using ThreeDeeInfrastructure.Repositories;
using ThreeDeeInfrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ThreeDeeFrontend.Areas.Identity;
using ThreeDeeFrontend.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddScoped<TopMenuViewModel>();
builder.Services.AddScoped<IJsInteropService<ModelRenderer>, JsInteropService<ModelRenderer>>();
builder.Services.AddScoped<IThemeProviderService, ThemeProviderService>();
builder.Services.AddScoped<IGCodeSettingsRepository, GCodeSettingsRepository>();
builder.Services.AddScoped<IFileRepository>(sp => new FileRepository(sp.GetService<HttpClient>()!, sp.GetService<IConfiguration>()!["UsersEndpoint"]));
builder.Services.AddScoped<IFilesGridViewModel, FilesGridViewModel>();

var appSettingsFilePath = builder.Environment.EnvironmentName == "Production" ? "appsettings.json" : "appsettings.Development.json";
var configurationRoot = new ConfigurationBuilder().AddJsonFile(appSettingsFilePath).Build();

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    string clientIdEnv = Environment.GetEnvironmentVariable("GOOGLE_OAUTH_CLIENT_ID");
    string clientId = string.IsNullOrEmpty(clientIdEnv) 
        ? configurationRoot.GetSection("Secrets:Google")["ClientId"] 
          ?? throw new InvalidOperationException("Client ID not found.")
        : clientIdEnv;
    
    string clientSecretEnv = Environment.GetEnvironmentVariable("GOOGLE_OAUTH_CLIENT_SECRET");
    string clientSecret = string.IsNullOrEmpty(clientSecretEnv) 
        ? configurationRoot.GetSection("Secrets:Google")["ClientSecret"] 
          ?? throw new InvalidOperationException("Client secret not found.")
        : clientSecretEnv;

    googleOptions.ClientId = clientId;
    googleOptions.ClientSecret = clientSecret;
});

builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
{
    string clientIdEnv = Environment.GetEnvironmentVariable("MICROSOFT_OAUTH_CLIENT_ID");
    string clientId = string.IsNullOrEmpty(clientIdEnv) 
        ? configurationRoot.GetSection("Secrets:Microsoft")["ClientId"] 
          ?? throw new InvalidOperationException("Client ID not found.")
        : clientIdEnv;
    
    string clientSecretEnv = Environment.GetEnvironmentVariable("MICROSOFT_OAUTH_CLIENT_SECRET");
    string clientSecret = string.IsNullOrEmpty(clientSecretEnv) 
        ? configurationRoot.GetSection("Secrets:Microsoft")["ClientSecret"] 
          ?? throw new InvalidOperationException("Client secret not found.")
        : clientSecretEnv;

    microsoftOptions.ClientId = clientId;
    microsoftOptions.ClientSecret = clientSecret;
});

builder.Services.AddAuthentication().AddGitHub(gitHubOptions =>
{
    string clientIdEnv = Environment.GetEnvironmentVariable("GITHUB_OAUTH_CLIENT_ID");
    string clientId = string.IsNullOrEmpty(clientIdEnv) 
        ? configurationRoot.GetSection("Secrets:GitHub")["ClientId"] 
          ?? throw new InvalidOperationException("Client ID not found.")
        : clientIdEnv;
    
    string clientSecretEnv = Environment.GetEnvironmentVariable("GITHUB_OAUTH_CLIENT_SECRET");
    string clientSecret = string.IsNullOrEmpty(clientSecretEnv) 
        ? configurationRoot.GetSection("Secrets:GitHub")["ClientSecret"] 
          ?? throw new InvalidOperationException("Client secret not found.")
        : clientSecretEnv;

    gitHubOptions.ClientId = clientId;
    gitHubOptions.ClientSecret = clientSecret;
    gitHubOptions.CallbackPath = "/signin-github";
    gitHubOptions.Scope.Add("read:user");
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();