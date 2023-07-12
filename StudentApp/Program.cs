using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StudentApp;
using StudentApp.Interfaces;
using StudentApp.Services;
using StudentApp.Services.Interfaces;
using StudentApp.ViewModels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var viewModelTypes = typeof(Program).Assembly.GetTypes()
    .Where(x => x.Name.EndsWith("ViewModel")).Where(x => x.IsAbstract is false)
    .Where(x => x.IsClass is true).Where(x => x.IsNested is false)
    .Where(x => x.IsSubclassOf(typeof(BaseViewModel)));

foreach (var type in viewModelTypes)
{
    builder.Services.AddScoped(type);
}

builder.Services
    .AddScoped<ILocalStorageService, LocalStorageService>()
    .AddScoped<IPracticeSessionService, PracticeSessionService>()
    .AddScoped<IAuthenticationService, AuthenticationService>()
    .AddScoped<IHttpService, HttpService>()
    .AddScoped<IUserService, UserService>();

builder.Services.AddScoped<HttpClient>(s => {
    var httpClient = new HttpClient();
    httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    return httpClient;
});

await builder.Build().RunAsync();