using FinalAssetManagement.Application.Mappings;
using FinalAssetManagement.Application.Services;
using FinalAssetManagement.Application.Services.Interfaces;
using FinalAssetManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(
       options => options.UseSqlServer(builder.Configuration.GetConnectionString("FinalAssetMngConn")));


// Add Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


// Inject Services
// Transient: Created every time they are requested from the service container.
// Scoped: Created once per client request (HTTP request), ideal for database contexts and business services.
// Singleton: Lives for the entire application lifetime.
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
