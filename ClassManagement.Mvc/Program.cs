using ClassManagement.Mvc.Configurations;
using ClassManagement.Mvc.Utilities;
using Utilities.Common;

var builder = WebApplication.CreateBuilder(args);

PolicyConfig.ConfigurePolicy(builder.Services);

builder.Services.AddHttpClient();

AuthenConfig.ConfigureAuthen(builder.Services);

builder.Services.AddMvc(options => { options.EnableEndpointRouting = false; });

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDistributedMemoryCache();

DIConfig.ConfigureDI(builder.Services);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseCookiePolicy();

app.UseSession();

app.UseSessionMiddleware();

RouteConfig.ConfigureRoute(app);

app.UseCors(PolicyConstants.CORS_NAME);

app.Run();
