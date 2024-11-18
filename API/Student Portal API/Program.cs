using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Portal.Models.Configuration;
using Student.Portal.Models.DataBase;
using Student.Portal.Models.Services;
using Student_Portal_API.Controllers.v1.Interface;
using Student_Portal_API.Controllers.v1.Model;
using Student_Portal_API.Controllers.v1.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Student.Portal.Models.Api;
using Student.Portal.Models.Binder;
using static Student.Portal.Models.Helpers.Constants;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://dashboard-portal.local:8080")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddApiVersioning(opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration configuration Sections at service loader
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(nameof(JwtConfiguration)));
builder.Services.Configure<APIEnvironment>(builder.Configuration.GetSection(nameof(APIEnvironment)));


// Transient instances
builder.Services.AddTransient<IDbAuthService, DbAuthService>();

// Add Database Context Configuration context
var databaseOptions = new DbContextOptionsBuilder<DatabaseConnection>();
databaseOptions.UseMySQL(builder.Configuration.GetConnectionString("DatabaseConnection"));
builder.Services.AddSingleton(databaseOptions.Options);

// Add Foundation Services 
builder.Services.AddSingleton<IDatabaseFactory<DatabaseConnection>, DatabaseFactory<DatabaseConnection>>();
builder.Services.AddSingleton<ITokenService, TokenService>();

// Add Business Logice Services
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration[$"{nameof(JwtConfiguration)}:{nameof(JwtConfiguration.Issuer)}"],
        ValidAudience = builder.Configuration[$"{nameof(JwtConfiguration)}:{nameof(JwtConfiguration.Audience)}"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[$"{nameof(JwtConfiguration)}:{nameof(JwtConfiguration.Key)}"]))
    };
    x.Events = new JwtBearerEvents
    {
        OnChallenge = async (context) =>
        {
            context.HandleResponse();
            if (context.AuthenticateFailure != null)
            {
                context.Response.StatusCode = 406;
                await context.HttpContext.Response.WriteAsJsonAsync(ApiResponse<Response>.GetErrorResponse(InternalCode.TokenAccess, System.Net.HttpStatusCode.InternalServerError, "Expired Token", new Exception()));
            }

        }
    };
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
