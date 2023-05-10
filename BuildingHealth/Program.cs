using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using BuildingHealth.BLL.Services;
using BuildingHealth.BLL.Interfaces;
using BuildingHealth.Core.Models;
using BuildingHealth.DAL;
using BuildingHealth.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<BuildingHealthDBContext>(options =>
    options.UseSqlServer("Data Source=ZINCHENKO-D2\\SQL2016;Initial Catalog=BuildingHealthDB;Integrated Security=True"));


builder.Services.AddScoped<IUserManagerService, UserManagerService>();
builder.Services.AddScoped<IRecomendationService, RecomendationService>();
builder.Services.AddScoped<IChartDataService, ChartDataService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IUserAccessor, UserAccessor>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers().AddNewtonsoftJson(x =>
 x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Building Health API",
        Version = "v1",
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddIdentityCore<User>(options => { options.Password.RequireNonAlphanumeric = false; })
    .AddEntityFrameworkStores<BuildingHealthDBContext>()
    .AddSignInManager<SignInManager<User>>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/api/UserManager/Register");
    });
builder.Services.AddTransient<IAuthorizationHandler, IsAdminRequirementHandler>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddAuthorization(opts => {
    opts.AddPolicy("IsAdmin",
        policy => policy.Requirements.Add(new IsAdminRequirement(true)));
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAllHeaders");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<BuildingHealthDBContext>();
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    //await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);
}


app.Run();
