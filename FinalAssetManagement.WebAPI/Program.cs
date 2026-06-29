using FinalAssetManagement.Application.Services;
using FinalAssetManagement.Application.Common.Interfaces;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.Application.Validations.Asset;
using FinalAssetManagement.Contract.Repositories;
using FinalAssetManagement.Infrastructure.Authentication;
using FinalAssetManagement.Infrastructure.Persistence;
using FinalAssetManagement.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FinalAssetManagement.Infrastructure.Security;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FinalAssetManagement.Application.Mappings;
using FluentValidation;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

//----------------------------------------------------------------------------------------------------------

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Register Swagger generator services
builder.Services.AddOpenApi();

//----------------------------------------------------------------------------------------------------------

// Add Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//----------------------------------------------------------------------------------------------------------

// Inject Services
// Transient: Created every time they are requested from the service container.
// Scoped: Created once per client request (HTTP request), ideal for database contexts and business services.
// Singleton: Lives for the entire application lifetime.

builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

//----------------------------------------------------------------------------------------------------------

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(
       options => options.UseSqlServer(builder.Configuration.GetConnectionString("FinalAssetMngConn")));

//----------------------------------------------------------------------------------------------------------

// FluentValidation registration
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAssetDtoValidator>();

//----------------------------------------------------------------------------------------------------------

builder.Services.AddEndpointsApiExplorer();

//----------------------------------------------------------------------------------------------------------

// Jwt Settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration
                         .GetSection("JwtSettings")
                         .Get<JwtSettings>() 
                         ?? throw new InvalidOperationException("JwtSettings section is missing or invalid.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
    };
});

//----------------------------------------------------------------------------------------------------------

var app = builder.Build();

//----------------------------------------------------------------------------------------------------------

// Configure the HTTP request pipeline.
// Enable OpenAPI/Swagger UI in Development environment
// An endpoint is a specific URL/route + an HTTP method that maps to an API operation.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

//----------------------------------------------------------------------------------------------------------
