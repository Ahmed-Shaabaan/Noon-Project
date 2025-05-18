using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation;
using ITI.E_Commerce.Presentation.IRepository;
using ITI.E_Commerce.Presentation.IRepository.BrandR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseLazyLoadingProxies()
     .UseLazyLoadingProxies()
        .UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddIdentity<Customer, IdentityRole>().AddEntityFrameworkStores<MyContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/SignIn";
    options.AccessDeniedPath = "/User/NotAuthorized";
});
builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequiredLength = 4;
    option.Password.RequiredUniqueChars = 0;
    option.SignIn.RequireConfirmedAccount = false;
});
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<CustomerIRepository, CustomerRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddLocalization();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStrinLocalizerFactory>();
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(JsonStrinLocalizerFactory));
    });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo(name:"en-US"),
        new CultureInfo(name:"ar-EG")
    };
    options.DefaultRequestCulture = new RequestCulture(supportedCultures[0], uiCulture: supportedCultures[0]);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

//allow browser to call folder content 
app.UseStaticFiles(new StaticFileOptions()
{
    RequestPath = "/wwwroot",
    FileProvider = new PhysicalFileProvider
    //Get current Directory of machine 
    (Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
});

app.UseRouting();

var supportedCultures = new[] { "en-Us", "ar-EG" };
var lozalizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(lozalizationOptions);


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
