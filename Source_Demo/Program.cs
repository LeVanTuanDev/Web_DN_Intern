using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using System.Net.Http.Headers;
using Source_Demo.Lib;
using Source_Demo.Models;
using Source_Demo.Services;
using System.Net.Http;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

void GetDefaultHttpClient(IServiceProvider serviceProvider, HttpClient httpClient, string hostUri)
{
    if (!string.IsNullOrEmpty(hostUri))
        httpClient.BaseAddress = new Uri(hostUri);
    httpClient.Timeout = TimeSpan.FromMinutes(1);
    httpClient.DefaultRequestHeaders.Clear();
    httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml+json");
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}

HttpClientHandler GetDefaultHttpClientHandler()
{
    return new HttpClientHandler
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
        UseCookies = false,
        AllowAutoRedirect = false,
        UseDefaultCredentials = true,
        ClientCertificateOptions = ClientCertificateOption.Manual,
        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true,
    };
}

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/dang-nhap";
    options.AccessDeniedPath = "/dang-nhap";
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.Name = "Session";
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClient("base")
    .ConfigureHttpClient((serviceProvider, httpClient) => GetDefaultHttpClient(serviceProvider, httpClient, builder.Configuration.GetSection("ApiSettings:UrlApi").Value))
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .ConfigurePrimaryHttpMessageHandler(x => GetDefaultHttpClientHandler());

builder.Services.AddHttpClient("custom")
    .ConfigureHttpClient((serviceProvider, httpClient) => GetDefaultHttpClient(serviceProvider, httpClient, string.Empty))
    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
    .ConfigurePrimaryHttpMessageHandler(x => GetDefaultHttpClientHandler());

builder.Services.AddSingleton<ICallApi, CallApi>();

builder.Services.AddSingleton<IS_Account, S_Account>();

builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseStatusCodePagesWithReExecute("/error/{0}");
    app.UseHsts();
}

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 7 * 60 * 60 * 24;
        ctx.Context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] =
            "public,max-age=" + durationInSeconds;
    }
});

app.UseCookiePolicy(); ;

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Home IsHot",
        pattern: "trang-chu",
        defaults: new { controller = "Home", action = "Index" });

    endpoints.MapControllerRoute(
       name: "Error page",
       pattern: "error/{code}",
       defaults: new { controller = "Error", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Login",
        pattern: "dang-nhap",
        defaults: new { controller = "A_Account", action = "P_Login" });

    endpoints.MapControllerRoute(
        name: "Register",
        pattern: "dang-ky",
        defaults: new { controller = "A_Account", action = "P_Register" });

    endpoints.MapControllerRoute(
    name: "Logout",
    pattern: "dang-xuat",
    defaults: new { controller = "A_Account", action = "P_Logout" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
