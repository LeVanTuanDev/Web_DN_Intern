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
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie = new CookieBuilder
    {
        //Domain = "cms.labadalat.com", //Releases in active
        Name = "AuthCMS",
        HttpOnly = true,
        Path = "/",
        SameSite = SameSiteMode.Lax,
        SecurePolicy = CookieSecurePolicy.Always
    };
    options.LoginPath = new PathString("/dang-nhap");
    options.ReturnUrlParameter = "";
    //options.LogoutPath = new PathString("/Account/SignOut");
    //options.AccessDeniedPath = new PathString("/Error/403");
    options.SlidingExpiration = true;
    options.Cookie.IsEssential = true;
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.Redirect(options.LoginPath);
        return Task.CompletedTask;
    };
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
builder.Services.AddSingleton<IS_NewsManage, S_NewsManage>();

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
        defaults: new { controller = "Account", action = "P_Login" });

    endpoints.MapControllerRoute(
        name: "Register",
        pattern: "dang-ky",
        defaults: new { controller = "Account", action = "P_Register" });

    endpoints.MapControllerRoute(
    name: "Logout",
    pattern: "dang-xuat",
    defaults: new { controller = "Account", action = "P_Logout" });

    endpoints.MapControllerRoute(
        name: "Field Manage",
        pattern: "quan-ly-linh-vuc-hoat-dong",
        defaults: new { controller = "FieldManage", action = "Index" });

    endpoints.MapControllerRoute(
       name: "Post Manage",
       pattern: "quan-ly-bai-viet",
       defaults: new { controller = "PostManage", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Recruit Manage",
        pattern: "quan-ly-tuyen-dung",
        defaults: new { controller = "RecruitManage", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Recruit Category",
        pattern: "danh-muc-tuyen-dung",
        defaults: new { controller = "RecruitCategory", action = "Index" });

    endpoints.MapControllerRoute(
        name: "News Manage",
        pattern: "quan-ly-tin-tuc",
        defaults: new { controller = "NewsManage", action = "Index" });

    endpoints.MapControllerRoute(
        name: "News Category",
        pattern: "danh-muc-tin-tuc",
        defaults: new { controller = "NewsCategory", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Image News",
        pattern: "hinh-anh-tin-tuc",
        defaults: new { controller = "ImageNews", action = "Index" });

    endpoints.MapControllerRoute(
       name: "Image Library",
       pattern: "hinh-anh-thu-vien",
       defaults: new { controller = "ImageLibrary", action = "Index" });

    endpoints.MapControllerRoute(
       name: "Image Post",
       pattern: "hinh-anh-bai-viet",
       defaults: new { controller = "ImagePost", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Library Manage",
        pattern: "quan-ly-thu-vien",
        defaults: new { controller = "LibraryManage", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Library Category",
        pattern: "danh-muc-thu-vien",
        defaults: new { controller = "LibraryCategory", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Partner Category",
        pattern: "danh-muc-doi-tac",
        defaults: new { controller = "PartnerCategory", action = "Index" });

    endpoints.MapControllerRoute(
        name: "Partner Manage",
        pattern: "quan-ly-doi-tac",
        defaults: new { controller = "PartnerManage", action = "Index" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
