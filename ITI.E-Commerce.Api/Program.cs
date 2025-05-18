using ITI.E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ITI.E_Commerce.Presentation.IRepository;
using ITI.E_Commerce.Api.IRepository;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;
using ITI.E_Commerce.Api.Helper;
using ITI.E_Commerce.Api.Interfaces;
using ITI.E_Commerce.Api.IRepositary;
using ITI.E_Commerce.Api.Repositories;
using Microsoft.Extensions.Options;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseLazyLoadingProxies()

        .UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddIdentity
    <Customer, IdentityRole>()
    .AddEntityFrameworkStores<MyContext>()
         .AddDefaultTokenProviders();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddAuthentication
    (
        options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
    .AddJwtBearer(
        options =>
        {

            options.TokenValidationParameters
            = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                SaveSigninToken = true,
                IssuerSigningKey
                 = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("IOLJYHSDSIoleJHsdsdsas98WeWsdsdQweweHgsgdf_&6#2"))
            };
        }
    );
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IOrderRepositoryApi, OrderRepositoryApi>();
builder.Services.
AddSwaggerGen(options =>
{
    options.
SwaggerDoc(
 name: "v1",
info: new OpenApiInfo
{
    Version = "v1",
    Title = "Swagger",
    Description = "Final Project",
    //TermsOfService = new Uri(uriString: "https://www.facebook.com/"),
    Contact = new Microsoft.OpenApi.Models.OpenApiContact
    {
        Name = "Team-3",
        Email = "ahmednowar55@gmail.com",
        //Url = new Uri(uriString: "https://www.facebook.com/")

    },
    License = new Microsoft.OpenApi.Models.OpenApiLicense
    {
        Name = "ITI E-Commerce Noon-WebSite",
        //Url = new Uri(uriString: "https://www.facebook.com/")
    }

});
    options.AddSecurityDefinition(name: "Bearer"
        ,
        securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter you Jwt Key"
        }
                );
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id= "Bearer"
                },
                Name= "Bearer",
                In = ParameterLocation.Header

            },
            new List<String>()
            }
            });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(i =>
    {
        i.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddScoped<ICustomerRepository, CustomerRepositoryd>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequiredLength = 4;
    option.Password.RequiredUniqueChars = 0;
    option.SignIn.RequireConfirmedAccount = false;
    option.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
});
builder.Services.Configure<DataProtectionTokenProviderOptions>
            (options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(5);
            });


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<CustomerIRepository, CustomerRepository>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();
    var userManager = service.GetRequiredService<UserManager<Customer>>();
    var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
    try
    {
        //Ensure Database Creation and Update to Latest Migration
        var context = service.GetRequiredService<MyContext>();
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An Error Ocurred During Migration");
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
