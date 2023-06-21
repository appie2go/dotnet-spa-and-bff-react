using GoCloudNative.Bff.Authentication.ModuleInitializers;
using GoCloudNative.Bff.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// !! Register the authentication gateway here
builder.Services.AddSecurityBff(o =>
{
    o.ConfigureOpenIdConnect(builder.Configuration.GetSection("OIDC"));
    o.LoadYarpFromConfig(builder.Configuration.GetSection("ReverseProxy"));
});
// !!

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// !! Register the authentication gateway here too
app.UseSecurityBff();

// And remove this
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
