using CRMBackend.Data.Interface;
using CRMBackend.Data.Models;
using CRMBackend.Data.Repositories;
using CRMBackend.Data.Repository;
using CRMBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add database connection string
builder.Services.AddDbContext<RMbackendContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddRoles<IdentityRole>().AddEntityFrameworkStores<RMbackendContext>().AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddTransient<IAccountRepo, AccountRepo>();
builder.Services.AddTransient<IAdministratorRepo, AdministratorRepo>();
builder.Services.AddTransient<IAWSImageService, AWSImageService>();
builder.Services.AddTransient<IBaseRepo, BaseRepo>();
builder.Services.AddTransient<ICategoryRepo, CategoryRepo>();
builder.Services.AddTransient<IContactRepo, ContactRepo>();
builder.Services.AddTransient<IEventRepo, EventRepo>();
builder.Services.AddTransient<IGroupRepo, GroupRepo>();
builder.Services.AddTransient<IReminderRepo, ReminderRepo>();
builder.Services.AddTransient<ISubCategoryRepo, SubCategoryRepo>();
builder.Services.AddTransient<IUserDocumentRepo, UserDocumentRepo>();
builder.Services.AddTransient<IUserEmailRepo, UserEmailRepo>();
builder.Services.AddTransient<IUserNoteRepo, UserNoteRepo>();
builder.Services.AddTransient<IUserPhotoRepo, UserPhotoRepo>();
builder.Services.AddTransient<IUserSMSRepo, UserSMSRepo>();
builder.Services.AddTransient<IContactEventRepo, ContactEventRepo>();
builder.Services.AddTransient<IContactGroupRepo, ContactGroupRepo>();

var jwtSection = builder.Configuration.GetSection("JwtBearerTokenSettings");
builder.Services.Configure<JwtBearerTokenSettings>(jwtSection);
var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRMbackend", Version = "v1" });
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
