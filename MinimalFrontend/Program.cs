using Microsoft.Fast.Components.FluentUI;
using MinimalFrontend.Controller;
using MinimalFrontend.Services;
using MinimalFrontend.ViewModels;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddScoped<TopMenuViewModel>();
builder.Services.AddScoped<IJsInteropService, JsInteropService>();
builder.Services.AddScoped<IThemeProviderService, ThemeProviderService>();
builder.Services.AddScoped<IFileRepository>(sp => new FileRepository(
    sp.GetService<HttpClient>()!, sp.GetService<IConfiguration>()!["UsersEndpoint"]));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();