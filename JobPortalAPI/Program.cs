using System.Text;
using JobPortalAPI.Core.Enums;
using JobPortalAPI.Core.Interfaces;
using JobPortalAPI.Infractructure;
using JobPortalAPI.Infractructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobPortalAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    // Prevents from schemas duplicating [public class Model is used twice etc.]
    c.CustomSchemaIds(type => type.FullName!.Replace('+', '.'));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICandidatesService, CandidatesService>();
builder.Services.AddScoped<IEmployersService, EmployersService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

builder.Services.AddDbContext<JobPortalContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<JobPortalContext>();
    var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    if (!await db.Users.AnyAsync(u => u.Role == Role.Admin))
    {
        var email = config["AdminSeed:Email"];
        var password = config["AdminSeed:Password"];

        if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
        {
            await authService.CreateAdminAsync(email, password);
        }
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();