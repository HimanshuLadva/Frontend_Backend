using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebsiteCMS.Configurations;
using WebsiteCMS.DAL.Data.Interface;
using WebsiteCMS.DAL.Data.Models;
using WebsiteCMS.DAL.Data.Repositories;
using WebsiteCMS.DAL.Models;
using WebsiteCMS.Global.Configurations;

var builder = WebApplication.CreateBuilder(args);


DirectoryConfig.Init(builder.Environment.WebRootPath, builder.Environment.ContentRootPath);
// Add services to the container.
builder.Services.AddDbContext<WebsiteCMSDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddRoles<IdentityRole>().AddEntityFrameworkStores<WebsiteCMSDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IBaseRepository, BaseRepository>();

builder.Services.AddScoped<IBOTAnalyticsRepository, BOTAnalyticsRepository>();

builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IAdministratorRepository, AdministratorRepository>();
builder.Services.AddTransient<SCRMITagRepository, SCRMTagRepository>();
builder.Services.AddTransient<SCRMITemplateFieldRepository, SCRMTemplateFieldRepository>();
builder.Services.AddTransient<SCRMITemplateFieldTypeRepository, SCRMTemplateFieldTypeRepository>();
builder.Services.AddTransient<SCRMITemplateRepository, SCRMTemplateRepository>();
builder.Services.AddTransient<SCRMIFontFamilyRepository, SCRMFontFamilyRepository>();
builder.Services.AddTransient<SCRMITemplateLayoutRepository, SCRMTemplateLayoutRepository>();
builder.Services.AddTransient<SCRMIUserRepository, SCRMUserRepository>();
builder.Services.AddTransient<SCRMIUserMetaDataRepository, SCRMUserMetaDataRepository>();
builder.Services.AddTransient<SCRMIUserTemplateRepository, SCRMUserTemplateRepository>();
builder.Services.AddTransient<SCRMITemplateDownloadRepository, SCRMTemplateDownloadRepository>();
builder.Services.AddScoped<SCRMILanguageRepository, SCRMLanguageRepository>();
builder.Services.AddScoped<SCRMIBackgroundColorRepository, SCRMBackgroundColorRepository>();

var jwtSection = builder.Configuration.GetSection("JwtBearerTokenSettings");
builder.Services.Configure<JwtBearerTokenSettings>(jwtSection);
var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>();
var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

var facebookSection = builder.Configuration.GetSection("FacebookAppSettings");
builder.Services.Configure<FacebookSettingsModel>(facebookSection);
var facebookSettings = facebookSection.Get<FacebookSettingsModel>();

var AWSSection = builder.Configuration.GetSection("AwsConfiguration");
builder.Services.Configure<AWSConfigurationModel>(AWSSection);
var AWSConfiguration = AWSSection.Get<AWSConfigurationModel>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
});

builder.Services.AddRepository();
builder.Services.AddRepositoryServices();



builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.SaveTokens = true;
    //options.AppId = "533348741771469";
    //options.AppSecret = "f376a5bee91ede4e06b20bce62529681";

    // Given By Akash
    options.AppId = facebookSettings.AppId;
    options.AppSecret = facebookSettings.AppSecret;
    options.Events.OnTicketReceived = (context) =>
    {
        return Task.CompletedTask;
    };
    options.Events.OnCreatingTicket = (context) =>
    {
        return Task.CompletedTask;
    };
});

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Default Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = false;
//    options.Password.RequiredLength = 5;
//    options.Password.RequiredUniqueChars = 0;
//});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebsiteCMS", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});

var app = builder.Build();

if (app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<WebsiteCMSDbContext>();
        context.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

app.UseCors();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"Resources/WCMS/Temporary")),
    RequestPath = new PathString("/Preview")
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();