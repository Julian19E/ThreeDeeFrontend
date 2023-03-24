using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using ThreeDeeFrontend.Components;
using ThreeDeeFrontend.ViewModels;
using ThreeDeeInfrastructure.Repositories;
using ThreeDeeInfrastructure.Services;

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