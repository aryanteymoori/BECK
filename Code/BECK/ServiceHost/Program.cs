using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_BECKQuery.Contract.Menu;
using _01_BECKQuery.Query;
using AccountManagement.Configuration;
using BlogManagement.Configuration;
using CommentManagement.Configuration;
using DiscountManagement.Configuration;
using InventoryManagement.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using ServiceHost;
using ServiceHost.Controllers;
using ShopManagement.Configuration;
using System.Drawing.Printing;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
var connectionString = builder.Configuration.GetConnectionString("BECK");

builder.Services.AddHttpContextAccessor();

ShopManagementBootstraper.Configure(builder.Services, connectionString);
DiscountManagementBootstraper.Configure(builder.Services, connectionString);
InventoryManagementBootstraper.Configure(builder.Services, connectionString);
BlogMangementBootStraper.Configure(builder.Services, connectionString);
CommentManagementBootstraper.Configure(builder.Services, connectionString);
AccountManagementBootstraper.Configure(builder.Services, connectionString);

builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IPasswordHasherMD5, PasswordHasherMD5>();
builder.Services.AddTransient<IMenuQuery, MenuQuery>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });



builder.Services.AddSingleton(
    HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic)
    );
builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminArea",
                    optionBuilder =>
                    optionBuilder.RequireRole(new List<string> { Roles.Administrator, Roles.ContentUploader }));

                //options.AddPolicy("ShopSystem", 
                //    optionBuilder =>
                //    optionBuilder.RequireRole(Roles.Administrator,Roles.ContentUploader));

                //options.AddPolicy("DiscountSystem",
                //   optionBuilder =>
                //   optionBuilder.RequireRole(Roles.Administrator));

                options.AddPolicy("AccountSystem",
                  optionBuilder =>
                  optionBuilder.RequireRole(Roles.Administrator));

                //options.AddPolicy("BlogingSystem",
                //  optionBuilder =>
                //  optionBuilder.RequireRole(Roles.Administrator));

                //options.AddPolicy("InventorySystem",
                //  optionBuilder =>
                //  optionBuilder.RequireRole(Roles.Administrator,Roles.ContentUploader));
            });

builder.Services.AddRazorPages()
    .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
        //options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "ShopSystem");
        //options.Conventions.AuthorizeAreaFolder("Administration", "/Discount", "DiscountSystem");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "AccountSystem");
        //options.Conventions.AuthorizeAreaFolder("Administration", "/Blog", "BlogingSystem");
        //options.Conventions.AuthorizeAreaFolder("Administration", "/Inventory", "InventorySystem");
    })
    .AddApplicationPart(typeof(PreviewPrintController).Assembly)
    .AddApplicationPart(typeof(InventoryController).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
